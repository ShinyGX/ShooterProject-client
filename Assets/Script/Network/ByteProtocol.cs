using System;
using System.Collections.Generic;

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
        if (bytes.Length - (index + lastOffset) < 0)
            return null;
        byte[] last = new byte[bytes.Length - (index + lastOffset)];
        Array.Copy(bytes, lastOffset + index, last, 0, last.Length);
        return last;
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

    public void Push(Fixed2 v)
    {
        Push(v.X.Value);
        Push(v.Y.Value);
    }

    public Fixed2 GetVector2()
    {
        Fixed x = GetFixed();
        Fixed y = GetFixed();
        return new Fixed2(x, y);
    }

    public void Push(Fixed v)
    {
        Push(v.Value);
    }

    public void Push(long int64)
    {
        byteList.AddRange(BitConverter.GetBytes(int64));
    }

    public Fixed GetFixed()
    {
        Fixed f = new Fixed();
        f.SetValue(GetInt64());
        return f;
    }

    public long GetInt64()
    {
        index += lastOffset;
        lastOffset = 8;
        return BitConverter.ToInt64(bytes, index);
    }
}
