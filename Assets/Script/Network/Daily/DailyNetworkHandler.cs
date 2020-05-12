using Daily;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class DailyNetworkHandler : NetworkHandler
{
    private GameCommandDispatch sender;

    public DailyNetworkHandler(GameCommandDispatch sender)
    {
        this.sender = sender;
    }

    public void ParseMessage(INetworkProtocol protocol)
    {
        if (!(protocol is ByteProtocol))
            return;

        RetType type = (RetType)protocol.GetByte();
        byte[] bytes = ((ByteProtocol)protocol).GetInputBytes();
        string json = Encoding.UTF8.GetString(bytes);

        switch (type)
        {
            case RetType.Login:
            case RetType.Register:
                sender.Send(JsonUtility.FromJson<LoginRet>(json));
                break;

            case RetType.Match:         
                sender.Send(JsonUtility.FromJson<MatchRet>(json));
                break;

            case RetType.Ready:
                sender.Send(JsonUtility.FromJson<ReadyRet>(json));
                break;
            case RetType.Matched:
                sender.Send(JsonUtility.FromJson<MatchedRet>(json));
                break;
            case RetType.Start:
                sender.Send(JsonUtility.FromJson<StartRet>(json));
                break;
        }

        Debug.Log(json);
        
    }
}
