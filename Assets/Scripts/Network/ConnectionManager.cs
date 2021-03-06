using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Net;
using System.Threading;
using UnityEngine;
using NetworkLibrary;
using NetworkLibrary.MessageElements;
using UnityEngine.SceneManagement;

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
    public List<LobbyStatusElement.PlayerInfo> playerInfo;

    private Boolean connected;
    private Boolean inLobby;
    private Boolean goToLogin;
    public Boolean gameStarted;
    private ElementId[] unreliableElementIds;
    public int ClientId {get;private set;} = -1;
    public int Team {get; set;} = 1;
    public bool GameOver {get; set;}

    private int playerNum;
    public int PlayerNum {get{return playerNum;} set {
        unreliableElementIds = new ElementId[value*3+2];
        int j = 0;
        for (int i = 0; i < value*3; i++)
        {  
            if(j == 0){
                unreliableElementIds[i] = ElementId.HealthElement;
            } else if (j == 1){
                unreliableElementIds[i] = ElementId.MovementElement;
            } else {
                j = -1;
                unreliableElementIds[i] = ElementId.ExperienceElement;
            }
            j++;
        }
        unreliableElementIds[unreliableElementIds.Length-2] = ElementId.RemainingLivesElement;
        unreliableElementIds[unreliableElementIds.Length-1] = ElementId.TowerHealthElement;
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
        socket = new UDPSocket(3);
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

        Packet confirmationPacket;

        try{
            confirmationPacket = socket.Receive();
        } catch (TimeoutException e){
            return false;
        }

        // Check if received packet is a confirmation packet
        if (ReliableUDPConnection.GetPacketType(confirmationPacket) != PacketType.ConfirmationPacket)
            return false;

        ClientId = ReliableUDPConnection.GetClientIdFromConfirmationPacket(confirmationPacket);
        ConnectReliableUDP();
        connected = true;
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
        if(inLobby) {
            Packet readyPacket = connection.CreatePacket(readyList, null, PacketType.HeartbeatPacket);
            socket.Send(readyPacket, destination);
        }
        if(goToLogin){
            goToLogin = false;
            SceneManager.LoadScene("login");
        }
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
        goToLogin = false;
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
            try{
                Packet ServerPacket = socket.Receive();
                UnpackedPacket UnpacketLobbyStatus = connection.ProcessPacket(ServerPacket, new ElementId[] {ElementId.LobbyStatusElement});
                UnpacketLobbyStatus.UnreliableElements.ForEach(ElementQueue.Enqueue);
                UnpacketLobbyStatus.ReliableElements.ForEach(ElementQueue.Enqueue);
            } catch (Exception e) {
                goToLogin = true;
                inLobby = false;
                return;
            }
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
    /// FUNCTION:	LobbyNetworking
    ///
    /// DATE:		April 6th, 2019
    ///
    /// REVISIONS:
    ///
    /// DESIGNER:	Cameron Roberts
    ///
    /// PROGRAMMER: Cameron Roberts
    ///
    /// INTERFACE: 	public void ExitLobbyToLogin()
    ///
    /// NOTES:		Exits the lobby state and goes to the login screen.
    ///             Resets the ConnectionManager
    /// ----------------------------------------------
    public void ExitLobbyToLogin()
    {
        inLobby = false;
        Reset();
        SceneManager.LoadScene("login");
    }


    /// ----------------------------------------------
    /// FUNCTION:	Reset
    ///
    /// DATE:		March 23rd, 2019
    ///
    /// REVISIONS:
    ///
    /// DESIGNER:	Simon Shoban
    ///
    /// PROGRAMMER:	Simon Shoban
    ///
    /// INTERFACE: 	public void Reset()
    ///
    /// NOTES:		Resets all the values in the ConnectionManager
    /// ----------------------------------------------
	public void Reset()
	{
		destination = new Destination();
        socket.Close();
		socket = new UDPSocket(3);
		connection = null;

		ClientId = -1;
		PlayerNum = 1;
		connected = false;
		inLobby = false;
        goToLogin = false;
		gameStarted = false;
		GameOver = false;

		MessageQueue = new ConcurrentQueue<UpdateElement>();
        ReliableElementQueue = new ConcurrentQueue<UpdateElement>();
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
        } else {
            SceneManager.LoadScene("login");
        }
    }

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
        while(true){
            try{

                Packet packet = socket.Receive();

                if(ReliableUDPConnection.GetPacketType(packet) != PacketType.GameplayPacket){
                    continue;
                }

                UnpackedPacket unpacked = connection.ProcessPacket(packet, unreliableElementIds);

                unpacked.UnreliableElements.ForEach(MessageQueue.Enqueue);
                unpacked.ReliableElements.ForEach(MessageQueue.Enqueue);
            } catch(Exception e){
                connected = false;
                Reset();
                return;
            }
        }

    }

}
