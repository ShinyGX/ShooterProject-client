using UnityEngine;

public class BattleNetworkHandler : NetworkHandler
{

    public void ParseMessage(INetworkProtocol protocol, int recursive = 0)
    {
        var mt = (MessageType)protocol.GetByte();
        switch (mt)
        {
            case MessageType.Init:
                Debug.Log("Get");

                break;
            case MessageType.End:
                break;
            default:
                Debug.Log("error message type : " + mt.ToString());
                break;
        }

        if (mt != MessageType.End && recursive < 5)
            ParseMessage(protocol, recursive + 1);

        if (protocol.Length > 0)
            Debug.Log("not parse: " + protocol.Length.ToString());

    }



    enum MessageType : byte
    {
        Init = 1,
        End = 255
    }
}
