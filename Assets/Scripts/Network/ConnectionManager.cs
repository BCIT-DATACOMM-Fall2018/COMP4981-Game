using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Net;
using System.Threading;
using UnityEngine;
using NetworkLibrary;
using NetworkLibrary.MessageElements;

public class ConnectionManager
{
    private Destination destination;
    private UDPSocket socket;
    private ReliableUDPConnection connection;
    public ConcurrentQueue<UpdateElement> MessageQueue {get; private set;}
    private ConcurrentQueue<UpdateElement> ReliableElementQueue {get; set;}

    private Boolean connected;
    private Boolean inLobby;
    private ElementId[] unreliableElementIds;
    public int ClientId {get;private set;} = -1;
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

    private ConnectionManager()
    {
        PlayerNum = 1;
        MessageQueue = new ConcurrentQueue<UpdateElement>();
        ReliableElementQueue = new ConcurrentQueue<UpdateElement>();
        CreateSocketUDP();
        InitializeConnection("127.0.0.1");
    }

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

    public void InitializeConnection(String stringIp){

        StartBackgroundNetworking(stringIp);
    }

    private class NestedConnectionManager
    {
        static NestedConnectionManager() { }
        internal static readonly ConnectionManager instance = new ConnectionManager();
    }

    private void CreateSocketUDP()
    {
        socket = new UDPSocket();
        socket.Bind();
    }

    private void ConnectReliableUDP()
    {
        connection = new ReliableUDPConnection(ClientId);
    }

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

    private void StartBackgroundNetworking(String stringIp){
        Thread backgroundRead = new Thread(() => BackgroundNetworking(stringIp));
        backgroundRead.Start();
    }


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
        readyList.Add(new ReadyElement(true, ClientId));
        Packet readyPacket = connection.CreatePacket(readyList, null, PacketType.HeartbeatPacket);
        socket.Send(readyPacket, destination);

        Packet startPacket = socket.Receive();
        UnpackedPacket unpackedStartPacket = connection.ProcessPacket(startPacket, new ElementId[] {});
        unpackedStartPacket.UnreliableElements.ForEach(MessageQueue.Enqueue);
        unpackedStartPacket.ReliableElements.ForEach(MessageQueue.Enqueue);


        //Lobby State
        /*
        while(true){
            try{
                Debug.Log("Waiting for packet in lobby");

                Packet packet = socket.Receive();
                Debug.Log("ReceivedPacket");
                UnpackedPacket unpacked = connection.ProcessPacket(packet, unreliableElementIds);

                unpacked.UnreliableElements.ForEach(MessageQueue.Enqueue);
                unpacked.ReliableElements.ForEach(MessageQueue.Enqueue);
            } catch(TimeoutException e){
                connected = false;
                return;
            }
        }
        */


        //Game State
        while(true){
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
