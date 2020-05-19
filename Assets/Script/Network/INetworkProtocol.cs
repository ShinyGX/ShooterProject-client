using System;

public interface INetworkProtocol 
{
    INetworkProtocol Init(byte[] bytes);
    int Length { get; }

    void Push(Int32 data);
    void Push(Int64 int64);
    void Push(byte b);
    void Push(byte[] b);
    void Push(Fixed2 v);
    void Push(Fixed v);

    Int32 GetInt32();
    Int64 GetInt64();
    byte GetByte();
    Fixed GetFixed();
    Fixed2 GetVector2();
    bool GetBool();


    byte[] OutputBytesStream();

}


