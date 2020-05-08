using System;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class NetworkHandler
{
    public NetworkConnection Connection
    {
        get
        {
            lock (connection)
            {
                return connection;
            }
        }
    }

    private NetworkConnection connection;

    public Queue<INetworkProtocol> MessageQue
    {
        get
        {
            lock(messageQue)
            {
                return messageQue;
            }
        }
    }

    private Queue<INetworkProtocol> messageQue = new Queue<INetworkProtocol>();

    public void Connect(string ip,int port)
    {
        connection = new NetworkConnection
        {
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp)
        };

        connection.socket.NoDelay = true;
        connection.socket.BeginConnect(ip, port, ConnectCallback, Connection);
    }

    private void ConnectCallback(IAsyncResult callback)
    {
        Connection.socket.BeginReceive(Connection.readBuff, 0, Connection.BufferRemain, SocketFlags.None, ReceiveCallback, Connection);
    }

    private void ReceiveCallback(IAsyncResult callback)
    {
        NetworkConnection conn = (NetworkConnection)callback.AsyncState;
        int len = conn.socket.EndReceive(callback);
        conn.recvLen = len;
        HandlerData(conn);
        conn.socket.BeginReceive(conn.readBuff, conn.recvLen, conn.BufferRemain, SocketFlags.None, ReceiveCallback, conn);
    }


    protected void HandlerData(NetworkConnection conn)
    {
        if (conn.recvLen < sizeof(Int32))
        {
            Debug.Log("Can not get message size" + conn.recvLen.ToString());
            return;
        }

        Array.Copy(conn.readBuff, conn.lenBytes, sizeof(Int32));
        conn.msgLen = BitConverter.ToInt32(conn.lenBytes, 0);

        if (conn.recvLen < conn.msgLen + sizeof(Int32))
        {
            Debug.Log("Package size error " + conn.recvLen.ToString() + ":" + (conn.msgLen + 4).ToString());
            return;
        }

        INetworkProtocol msg = new ByteProtocol();
        msg.Init(conn.ReceiveBytes);
        MessageQue.Enqueue(msg);

        int count = conn.recvLen - conn.msgLen - sizeof(Int32);
        Array.Copy(conn.readBuff, sizeof(Int32) + conn.msgLen, conn.readBuff, 0, count);
        conn.recvLen = count;
        if (conn.recvLen > 0)
            HandlerData(conn);
    }
    public void SendMessage(byte[] bytes)
    {
        Int32 len = bytes.Length;
        byte[] lenBytes = BitConverter.GetBytes(len);
        byte[] temp = new byte[len + 4];
        Array.Copy(lenBytes, 0, temp, 0, 4);
        Array.Copy(bytes, 0, temp, 4, len);
        Connection.socket.BeginSend(temp, 0, temp.Length, SocketFlags.None, null, null);
    }

    public virtual void Stop()
    {
        Connection.socket.Close();
    }



}
