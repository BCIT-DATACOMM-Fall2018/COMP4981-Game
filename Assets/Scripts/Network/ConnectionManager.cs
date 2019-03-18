using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Net;
using System.Threading;
using UnityEngine;
using NetworkLibrary;
using NetworkLibrary.MessageElements;

/// ----------------------------------------------
/// Interface: 	ConnectionManager - A singleton class to manage a connection
///                                 to the server
/// 
/// PROGRAM: SKOM
///
/// CONSTRUCTORS:	private ConnectionManager()
/// 
/// FUNCTIONS:	TBD
/// 
/// DATE: 		March 14th, 2019
///
/// REVISIONS: 
///
/// DESIGNER: 	Simon Wu, Simon Shoban, Cameron Roberts
///
/// PROGRAMMER: Simon Wu, Simon Shoban, Cameron Roberts
///
/// NOTES:		
/// ----------------------------------------------
public class ConnectionManager
{
    private Destination destination;
    private UDPSocket socket;
    private ReliableUDPConnection connection;
    public ConcurrentQueue<UpdateElement> MessageQueue {get; private set;}
    private ConcurrentQueue<UpdateElement> ReliableElementQueue {get; set;}

    private Boolean connected;
    private Boolean inLobby;
    public Boolean gameStarted;
    private ElementId[] unreliableElementIds;
    public int ClientId {get;private set;} = -1;
    public int Team {get; set;} = 2;
    private int playerNum;
    public int PlayerNum {get{return playerNum;} set { 
        unreliableElementIds = new ElementId[value*2];
        for (int i = 0; i < value*2; i++)
        {
            if(i % 2 == 0){
                unreliableElementIds[i] = ElementId.HealthElement;
            } else{
                unreliableElementIds[i] = ElementId.MovementElement;
            }
        }
        playerNum = value;
    }}


    /// ----------------------------------------------
	/// CONSTRUCTOR: ConnectionManager
	/// 
	/// DATE: 		March 14th, 2019
	/// 
	/// REVISIONS:	
	/// 
	/// DESIGNER:	Simon Shoban, Simon Wu
	/// 
	/// PROGRAMMER:	Simon Shoban, Simon Wu
	/// 
	/// INTERFACE: 	private ConnectionManager ()
	/// 
	/// NOTES:		Default constructor. Private to maintain singleton
	/// ----------------------------------------------
    private ConnectionManager()
    {
        PlayerNum = 1;
        MessageQueue = new ConcurrentQueue<UpdateElement>();
        ReliableElementQueue = new ConcurrentQueue<UpdateElement>();
        CreateSocketUDP();
        InitializeConnection("127.0.0.1");
    }

	/// ----------------------------------------------
    /// FUNCTION:	QueueReliableElement
    /// 
    /// DATE:		March 14th, 2019
    /// 
    /// REVISIONS:	
    /// 
    /// DESIGNER:	Cameron Roberts
    /// 
    /// PROGRAMMER:	Cameron Roberts
    /// 
    /// INTERFACE: 	public void QueueReliableElement(UpdateElement element)
	///					UpdateElement element: The UpdateElement to add to the queue
    /// 
    /// NOTES:		Any UpdateElement added to the queue will be sent to the server with
    ///             the next packet (assuming theres space in the packet)
    /// ----------------------------------------------
    public void QueueReliableElement(UpdateElement element){
        ReliableElementQueue.Enqueue(element);
    }

    public static ConnectionManager Instance
    {
        get
        {
            return NestedConnectionManager.instance;
        }
    }

    /// ----------------------------------------------
    /// FUNCTION:	InitializeConnection
    /// 
    /// DATE:		March 14th, 2019
    /// 
    /// REVISIONS:	
    /// 
    /// DESIGNER:	Simon Shoban, Simon Wu, Cameron Roberts
    /// 
    /// PROGRAMMER:	Simon Shoban, Simon Wu, Cameron Roberts
    /// 
    /// INTERFACE: 	public void InitializeConnection(String stringIp)
	///					String stringIp: The IP address of the server as a string
    /// 
    /// NOTES:		
    /// ----------------------------------------------
    public void InitializeConnection(String stringIp){

        StartBackgroundNetworking(stringIp);
    }

    private class NestedConnectionManager
    {
        static NestedConnectionManager() { }
        internal static readonly ConnectionManager instance = new ConnectionManager();
    }

    /// ----------------------------------------------
    /// FUNCTION:	CreateUDPSocket
    /// 
    /// DATE:		March 14th, 2019
    /// 
    /// REVISIONS:	
    /// 
    /// DESIGNER:	Simon Shoban, Simon Wu
    /// 
    /// PROGRAMMER:	Simon Shoban, Simon Wu
    /// 
    /// INTERFACE: 	private void CreateSocketUDP()
    /// 
    /// NOTES:		Creates and binds a UDP socket
    /// ----------------------------------------------
    private void CreateSocketUDP()
    {
        socket = new UDPSocket(15);
        socket.Bind();
    }

    /// ----------------------------------------------
    /// FUNCTION:	ConnectReliableUDP
    /// 
    /// DATE:		March 14th, 2019
    /// 
    /// REVISIONS:	
    /// 
    /// DESIGNER:	Simon Shoban, Simon Wu
    /// 
    /// PROGRAMMER:	Simon Shoban, Simon Wu
    /// 
    /// INTERFACE: 	private void CreateSocketUDP()
    /// 
    /// NOTES:		Initiates the ReliableUDPConnection
    /// ----------------------------------------------
    private void ConnectReliableUDP()
    {
        connection = new ReliableUDPConnection(ClientId);
    }

    /// ----------------------------------------------
    /// FUNCTION:	SendStatePacket
    /// 
    /// DATE:		March 14th, 2019
    /// 
    /// REVISIONS:	
    /// 
    /// DESIGNER:	Cameron Roberts
    /// 
    /// PROGRAMMER:	Cameron Roberts
    /// 
    /// INTERFACE: 	public void SendStatePacket(List<UpdateElement> gameState)
    /// 
    /// NOTES:		Takes the given UpdateElements and combines them
    ///             with UpdateElements from the ReliableElementQueue
    ///             and sends the packet to the server.
    /// ----------------------------------------------
    public void SendStatePacket(List<UpdateElement> gameState){
        if(connected){
            List<UpdateElement> reliableElements = new List<UpdateElement>();
            UpdateElement temp = null;
            while(ReliableElementQueue.TryDequeue(out temp)){
                reliableElements.Add(temp);
            }
            Packet packet = connection.CreatePacket(gameState, reliableElements);
            socket.Send(packet, destination);
        }
    }

    /// ----------------------------------------------
    /// FUNCTION:	StarBackgroundNetworking
    /// 
    /// DATE:		March 14th, 2019
    /// 
    /// REVISIONS:	
    /// 
    /// DESIGNER:	Cameron Roberts
    /// 
    /// PROGRAMMER:	Cameron Roberts
    /// 
    /// INTERFACE: 	private void StartBackgroundNetworking(String stringIp)
    /// 
    /// NOTES:		Creates and starts a thread that will perform network operations
    ///             in the background.
    /// ----------------------------------------------
    private void StartBackgroundNetworking(String stringIp){
        Thread backgroundRead = new Thread(() => BackgroundNetworking(stringIp));
        backgroundRead.Start();
    }


    /// ----------------------------------------------
    /// FUNCTION:	BackgroundNetworking
    /// 
    /// DATE:		March 14th, 2019
    /// 
    /// REVISIONS:	
    /// 
    /// DESIGNER:	Cameron Roberts
    /// 
    /// PROGRAMMER:	Cameron Roberts
    /// 
    /// INTERFACE: 	private void BackgroundNetworking(String stringIp)
    /// 
    /// NOTES:		Recieves packets from the server and queues elements
    ///             from the received packets.
    /// ----------------------------------------------
    private void BackgroundNetworking(String stringIp) {
        IPAddress address = IPAddress.Parse(stringIp);
        destination = new Destination((uint)BitConverter.ToInt32(address.GetAddressBytes(), 0), (ushort)System.Net.IPAddress.HostToNetworkOrder((short)8000));
        socket.Send(ReliableUDPConnection.CreateRequestPacket(), destination);
        Packet confirmationPacket = socket.Receive();
        ClientId = ReliableUDPConnection.GetClientIdFromConfirmationPacket(confirmationPacket);
        ConnectReliableUDP();

        connected = true;
        Debug.Log("Connected");

        List<UpdateElement> readyList = new List<UpdateElement>();
        readyList.Add(new ReadyElement(true, ClientId, Team));
        Packet readyPacket = connection.CreatePacket(readyList, null, PacketType.HeartbeatPacket);
        socket.Send(readyPacket, destination);

        Packet startPacket = socket.Receive();
        UnpackedPacket unpackedStartPacket = connection.ProcessPacket(startPacket, new ElementId[] {ElementId.LobbyStatusElement});
        unpackedStartPacket.UnreliableElements.ForEach(MessageQueue.Enqueue);
        unpackedStartPacket.ReliableElements.ForEach(MessageQueue.Enqueue);

        //Game State
        while(true){
            // THIS LINE EXISTS TO PREVENT A RACE CONDITION CAUSED BY PROCESSING A PACKET BEFORE THE START GAME
            // BRIDGE FUNCTION IS CALLED. THE IMPLEMENTATION OF A PROPER LOBBY STATE SHOULD FIX THE ISSUE AFTER
            // WHICH THIS CHECK CAN BE REMOVED.
            if(!gameStarted){
                continue;
            }
            try{
                Debug.Log("Waiting for packet");

                Packet packet = socket.Receive();

                if(ReliableUDPConnection.GetPacketType(packet) != PacketType.GameplayPacket){
                    continue;
                }

                Debug.Log("ReceivedPacket");
                UnpackedPacket unpacked = connection.ProcessPacket(packet, unreliableElementIds);

                unpacked.UnreliableElements.ForEach(MessageQueue.Enqueue);
                unpacked.ReliableElements.ForEach(MessageQueue.Enqueue);
            } catch(TimeoutException e){
                connected = false;
                return;
            }
        }

    }

}
