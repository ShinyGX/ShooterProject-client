using System;

public interface INetworkProtocol 
{
    INetworkProtocol Init(byte[] bytes);
    int Length { get; }

    void Push(Int32 data);
    void Push(byte b);
    void Push(byte[] b);

    Int32 GetInt32();
    byte GetByte();
   


    byte[] OutputBytesStream();

}


