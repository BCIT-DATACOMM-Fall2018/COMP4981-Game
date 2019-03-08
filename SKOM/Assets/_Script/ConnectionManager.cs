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
    private readonly ElementId[] unreliableElementIds = {ElementId.HealthElement};
    private int clientId = -1;

    private ConnectionManager()
    {
        MessageQueue = new ConcurrentQueue<UpdateElement>();
        CreateSocketUDP();
        ConnectReliableUDP();
        InitializeConnection("127.0.0.1");
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

    /*** Public functions ***/
    private void CreateSocketUDP()
    {
        socket = new UDPSocket();
        socket.Bind();
    }

    private void ConnectReliableUDP()
    {
        connection = new ReliableUDPConnection(0);
    }

    public void SendPacket(Packet packet)
    {
        socket.Send(packet, destination);
    }

    public Packet ReceivePacket()
    {
        return socket.Receive();
    }

    public Packet Packetize(List<UpdateElement> reliableElements, List<UpdateElement> unreliableElements)
    {
        return connection.CreatePacket(reliableElements, unreliableElements);
    }

    public UnpackedPacket UnPack(Packet packet, ElementId[] expectedUnreliableIds)
    {
        return connection.ProcessPacket(packet, expectedUnreliableIds);
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
        clientId = ReliableUDPConnection.GetPlayerID(confirmationPacket);
        while(true){
            Debug.Log("Waiting for packet");
            Packet packet = socket.Receive();
            Debug.Log("ReceivedPacket");
            UnpackedPacket unpacked = connection.ProcessPacket(packet, unreliableElementIds);
            unpacked.UnreliableElements.ForEach(MessageQueue.Enqueue);
            unpacked.ReliableElements.ForEach(MessageQueue.Enqueue);
        }
    }

}
