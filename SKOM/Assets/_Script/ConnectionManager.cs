using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NetworkLibrary;
using NetworkLibrary.MessageElements;

public class ConnectionManager
{
    private Destination destination;
    private UDPSocket socket;
    private ReliableUDPConnection connection;

    private ConnectionManager()
    {
        CreateSocketUDP();

        ConnectReliableUDP();

        destination = new Destination((uint)System.Net.IPAddress.HostToNetworkOrder(2130706433), 
            (ushort)System.Net.IPAddress.HostToNetworkOrder((short)8000));
    }

    public static ConnectionManager Instance
    {
        get
        {
            return NestedConnectionManager.instance;
        }
    }

    private class NestedConnectionManager
    {
        static NestedConnectionManager() { }
        internal static readonly ConnectionManager instance = new ConnectionManager();
    }

    /*** Public functions ***/
    public void CreateSocketUDP()
    {
        socket = new UDPSocket();
        socket.Bind();
    }

    public void ConnectReliableUDP()
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
}