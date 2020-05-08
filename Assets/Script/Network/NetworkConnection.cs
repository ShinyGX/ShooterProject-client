using System;
using System.Net.Sockets;

/// <summary>
/// 网络连接信息
/// </summary>
public class NetworkConnection
{
    public int clientId = 1;
    public readonly static int bufferSize = 1024;

    public byte[] readBuff = new byte[bufferSize];

    //数据长度(bytes)
    public byte[] lenBytes = new byte[4];

    //数据长度(int)
    public int msgLen = 0;


    public Socket socket;

    public int recvLen = 0;
    private byte[] tempByte;

    //Buff剩余长度
    public int BufferRemain
    {
        get
        {
            return bufferSize - msgLen;
        }
    }

    //接受的字节数
    public byte[] ReceiveBytes
    {
        get
        {
            tempByte = new byte[msgLen];
            Array.Copy(readBuff, 4, tempByte, 0, msgLen);
            return tempByte;
        }
    }




}
