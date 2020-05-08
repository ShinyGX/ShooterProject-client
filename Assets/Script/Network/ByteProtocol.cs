using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ByteProtocol : INetworkProtocol
{

    private List<Byte> byteList = new List<byte>();
    private byte[] bytes;
    private int index = 0;
    private int lastOffset = 0;
   // private Int16 strLen = 0;
    private byte[] tempBytes;

    public int Length
    {
        get
        {
            return bytes.Length - (index + lastOffset);
        }
    }

    public byte GetByte()
    {
        index += lastOffset;
        lastOffset = 1;
        return bytes[index];
    }

    public byte[] OutputBytesStream()
    {
        return byteList.ToArray();
    }

    public int GetInt32()
    {
        index += lastOffset;
        lastOffset = 4;
        return BitConverter.ToInt32(bytes, index);
    }

    public byte[] GetInputBytes()
    {
        return bytes;
    }

    public INetworkProtocol Init(byte[] bytes)
    {
        this.bytes = bytes;
        index = lastOffset = 0;
        return this;
    }

    public void Push(int data)
    {
        byteList.AddRange(BitConverter.GetBytes(data));
    }

    public void Push(byte b)
    {
        byteList.Add(b);
    }

    public void Push(byte[] b)
    {
        byteList.AddRange(b);
    }
}
