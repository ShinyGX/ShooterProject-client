using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class DailyNetworkHandler : NetworkHandler
{
    public void ParseMessage(INetworkProtocol protocol)
    {
        if (!(protocol is ByteProtocol))
            return;

        byte[] bytes = ((ByteProtocol)protocol).GetInputBytes();
        Debug.Log(bytes);
        string json = Encoding.Default.GetString(bytes);
        Debug.Log(json);
    }
}
