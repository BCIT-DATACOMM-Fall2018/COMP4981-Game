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
    public bool GameOver {get; set;}

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
        //InitializeConnection("127.0.0.1");
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

        StartBackgroundNetworking();
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
        socket = new UDPSocket(0);
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


    //
    //
    //
    //  T E M P   H E L P E R
    //
    //
    //
    public void InitConnection(String Ipaddress)
    {
        IPAddress address = IPAddress.Parse(Ipaddress);
        destination = new Destination((uint)BitConverter.ToInt32(address.GetAddressBytes(), 0), (ushort)System.Net.IPAddress.HostToNetworkOrder((short)8000));
        socket.Send(ReliableUDPConnection.CreateRequestPacket("Alice"), destination);
        Packet confirmationPacket = socket.Receive();
        ClientId = ReliableUDPConnection.GetClientIdFromConfirmationPacket(confirmationPacket);
        ConnectionManager.Instance.ConnectReliableUDP();
        connected = true;
    }

    /// ----------------------------------------------
    /// FUNCTION:	RequestConnection
    ///
    /// DATE:		March 14th, 2019
    ///
    /// REVISIONS:  April 1st, 2019
    ///
    /// DESIGNER:	Cameron Roberts
    ///
    /// PROGRAMMER:	Cameron Roberts, Viktor Alvar
    ///
    /// INTERFACE: 	private Boolean RequestConnection(String stringIp, String clientName)
    ///                 String stringIp: Ip address entered by client
    ///                 String clientName: Name entered by client
    ///
    /// RETURN:     Boolean. True if connection successful, otherwise False
    ///
    /// NOTES:		Requests initial connection to server. This function
    ///             is called when the client logs into the the lobby
    /// ----------------------------------------------
    public Boolean RequestConnection(String stringIp, String clientName) {
        IPAddress address = IPAddress.Parse(stringIp);
        destination = new Destination((uint)BitConverter.ToInt32(address.GetAddressBytes(), 0), (ushort)System.Net.IPAddress.HostToNetworkOrder((short)8000));
        socket.Send(ReliableUDPConnection.CreateRequestPacket(clientName), destination);
        // TODO: Receive call just blocks, introduce client or server timeout
        Packet confirmationPacket = socket.Receive();

        // Check if received packet is a confirmation packet
        if (ReliableUDPConnection.GetPacketType(confirmationPacket) != PacketType.ConfirmationPacket)
            return false;

        ClientId = ReliableUDPConnection.GetClientIdFromConfirmationPacket(confirmationPacket);
        ConnectReliableUDP();
        return true;
    }

    /// ----------------------------------------------
    /// FUNCTION:	SendLobbyHeartbeat
    ///
    /// DATE:		March 28h, 2019
    ///
    /// REVISIONS:
    ///
    /// DESIGNER:	Rhys Snaydon
    ///
    /// PROGRAMMER: Rhys Snaydon
    ///
    /// INTERFACE: 	public void SendLobbyHeartbeat(List<UpdateElement> readyList)
    ///
    /// NOTES:		Sends a hearbeat packet to the server to update the client's
    ///             ready status and team.
    /// ----------------------------------------------
    public void SendLobbyHeartbeat(List<UpdateElement> readyList)
    {
        Packet readyPacket = connection.CreatePacket(readyList, null, PacketType.HeartbeatPacket);
        socket.Send(readyPacket, destination);
    }

    /// ----------------------------------------------
    /// FUNCTION:	StarLobbyNetworking
    ///
    /// DATE:		March 28h, 2019
    ///
    /// REVISIONS:
    ///
    /// DESIGNER:	Rhys Snaydon
    ///
    /// PROGRAMMER: Rhys Snaydon
    ///
    /// INTERFACE: 	public void StartLobbyNetworking(LobbyStateMessageBridge StateBridge, ConcurrentQueue<UpdateElement> ElementQueue)
    ///
    /// NOTES:		Creates and starts a thread that will receive server updates
    ///             in the background of the lobby state.
    /// ----------------------------------------------
    public void StartLobbyNetworking(LobbyStateMessageBridge StateBridge, ConcurrentQueue<UpdateElement> ElementQueue)
    {
        inLobby = true;
        Thread backgroundReceive = new Thread(() => LobbyNetworking(StateBridge, ElementQueue));
        backgroundReceive.Start();
    }

    /// ----------------------------------------------
    /// FUNCTION:	LobbyNetworking
    ///
    /// DATE:		March 28h, 2019
    ///
    /// REVISIONS:
    ///
    /// DESIGNER:	Rhys Snaydon
    ///
    /// PROGRAMMER: Rhys Snaydon
    ///
    /// INTERFACE: 	public void StartLobbyNetworking(LobbyStateMessageBridge StateBridge, ConcurrentQueue<UpdateElement> ElementQueue)
    ///
    /// NOTES:		Receives LobbyStatusElements from the server and adds the to
    ///             a concurrent queue to be processed by the LobbyManager.
    /// ----------------------------------------------
    private void LobbyNetworking(LobbyStateMessageBridge StateBridge, ConcurrentQueue<UpdateElement> ElementQueue)
    {
        while (inLobby)
        {
            Debug.Log("Receiving in Lobby Networking");
            Packet ServerPacket = socket.Receive();
            Debug.Log("Unpacking packet in Lobby Networking");
            UnpackedPacket UnpacketLobbyStatus = connection.ProcessPacket(ServerPacket, new ElementId[] {ElementId.LobbyStatusElement});
            Debug.Log("Queueing elements in Lobby Networking");
            UnpacketLobbyStatus.UnreliableElements.ForEach(ElementQueue.Enqueue);
            UnpacketLobbyStatus.ReliableElements.ForEach(ElementQueue.Enqueue);
        }
    }

    /// ----------------------------------------------
    /// FUNCTION:	LobbyNetworking
    ///
    /// DATE:		April 1, 2019
    ///
    /// REVISIONS:
    ///
    /// DESIGNER:	Rhys Snaydon
    ///
    /// PROGRAMMER: Rhys Snaydon
    ///
    /// INTERFACE: 	public void ExitLobbyState(int PlayerCount)
    ///
    /// NOTES:		Sets up the game state and starts the background networking.
    /// ----------------------------------------------
    public void ExitLobbyState(int PlayerCount)
    {
        PlayerNum = PlayerCount;
        inLobby = false;
		gameStarted = true;
        StartBackgroundNetworking();
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
    /// FUNCTION:	Login
    ///
    /// DATE:		March 31th, 2019
    ///
    /// REVISIONS:
    ///
    /// DESIGNER:	Cameron Roberts
    ///
    /// PROGRAMMER:	Viktor Alvar
    ///
    /// INTERFACE: 	public void Login(String stringIp)
    ///
    /// NOTES:      Sends initial connect packet to the server and
    ///             loads the lobby scene if connection was successfull.
    /// ----------------------------------------------
    // private void Login(String stringIp){
    //     // TODO: Get User's Name

    //     // Create and Send Request Packet
    //     IPAddress address = IPAddress.Parse(stringIp);
    //     destination = new Destination((uint)BitConverter.ToInt32(address.GetAddressBytes(), 0), (ushort)System.Net.IPAddress.HostToNetworkOrder((short)8000));
    //     socket.Send(ReliableUDPConnection.CreateRequestPacket(), destination);

    //     // TODO: Load Lobby Scene
    //     // TODO: Pass ClientId to Lobby Scene
    //     // Receive Confirmation Packet and Establish Connection
    //     Packet confirmationPacket = socket.Receive();
    //     ClientId = ReliableUDPConnection.GetClientIdFromConfirmationPacket(confirmationPacket);
    //     ConnectReliableUDP();

    //     connected = true;
    //     Debug.Log("Connected");

    //     // Create ReadyElement for current client
    //     List<UpdateElement> readyList = new List<UpdateElement>();
    //     readyList.Add(new ReadyElement(true, ClientId, 0));

    //     // TODO: Keep Sending Heartbeat Packets until Game Start (maybe for Lobby Scene)
    //     // TODO: Client send ready or not read packets to server in (maybe for Lobby Scene)
    //     // Send Heartbeat Packet to let Server add current client to PlayerConnection array
    //     // The packet lets the server know the client is ready to start the game
    //     Packet readyPacket = connection.CreatePacket(readyList, null, PacketType.HeartbeatPacket);
    //     socket.Send(readyPacket, destination);

    //     // Receive the start packet
    //     // Start Packet contains Unreliable Elements which contians number of connections to server
    //     // Start Packet contains Reliable Elements which contains each client's properties
    //     Packet startPacket = socket.Receive();
    //     UnpackedPacket unpackedStartPacket = connection.ProcessPacket(startPacket, new ElementId[] { });
    //     unpackedStartPacket.UnreliableElements.ForEach(MessageQueue.Enqueue);
    //     unpackedStartPacket.ReliableElements.ForEach(MessageQueue.Enqueue);

    //     // TODO: Pass start packet elements to Lobby Scene to display each client
    // }

    /// ----------------------------------------------
    /// FUNCTION:	StarBackgroundNetworking
    ///
    /// DATE:		March 14th, 2019
    ///
    /// REVISIONS:	April 1st, 2019
    ///
    /// DESIGNER:	Cameron Roberts
    ///
    /// PROGRAMMER:	Cameron Roberts, Rhys Snaydon
    ///
    /// INTERFACE: 	private void StartBackgroundNetworking()
    ///
    /// NOTES:		Creates and starts a thread that will perform network operations
    ///             in the background.
    /// ----------------------------------------------
    private void StartBackgroundNetworking(){
        Thread backgroundRead = new Thread(() => BackgroundNetworking());
        backgroundRead.Start();
    }

    /// ----------------------------------------------
    /// FUNCTION:	BackgroundNetworking
    ///
    /// DATE:		March 14th, 2019
    ///
    /// REVISIONS:	April 1st, 2019
    ///
    /// DESIGNER:	Cameron Roberts
    ///
    /// PROGRAMMER:	Cameron Roberts, Rhys Snaydon
    ///
    /// INTERFACE: 	private void BackgroundNetworking()
    ///
    /// NOTES:		Recieves packets from the server and queues elements
    ///             from the received packets.
    /// ----------------------------------------------
    private void BackgroundNetworking() {
        // IPAddress address = IPAddress.Parse(stringIp);
        // destination = new Destination((uint)BitConverter.ToInt32(address.GetAddressBytes(), 0), (ushort)System.Net.IPAddress.HostToNetworkOrder((short)8000));
        // socket.Send(ReliableUDPConnection.CreateRequestPacket("Alice"), destination);
        // Packet confirmationPacket = socket.Receive();
        // ClientId = ReliableUDPConnection.GetClientIdFromConfirmationPacket(confirmationPacket);
        // ConnectReliableUDP();

        // connected = true;
        // Debug.Log("Connected");

        // List<UpdateElement> readyList = new List<UpdateElement>();
        // readyList.Add(new ReadyElement(true, ClientId, Team));
        // Packet readyPacket = connection.CreatePacket(readyList, null, PacketType.HeartbeatPacket);
        // socket.Send(readyPacket, destination);

        // Packet startPacket = socket.Receive();
        // UnpackedPacket unpackedStartPacket = connection.ProcessPacket(startPacket, new ElementId[] {ElementId.LobbyStatusElement});
        // unpackedStartPacket.UnreliableElements.ForEach(MessageQueue.Enqueue);
        // unpackedStartPacket.ReliableElements.ForEach(MessageQueue.Enqueue);

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
                    Debug.Log("This is breaking everything");
                    continue;
                }

                Debug.Log("ReceivedPacket");
                UnpackedPacket unpacked = connection.ProcessPacket(packet, unreliableElementIds);

                unpacked.UnreliableElements.ForEach(MessageQueue.Enqueue);  
                unpacked.ReliableElements.ForEach(MessageQueue.Enqueue);
            } catch(Exception e){
                Debug.Log(e.StackTrace);
                Debug.Log(e.Message);
                connected = false;
                return;
            }
        }

    }

}
