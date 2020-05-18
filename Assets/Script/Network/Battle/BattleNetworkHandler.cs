using System;
using UnityEngine;

public class BattleNetworkHandler : NetworkHandler
{

    public BattleGameLoop gameLoop;

    public Random random;

    protected override void Init()
    {
        gameLoop = new BattleGameLoop(this);
    }


    public void ParseMessage(INetworkProtocol protocol, int recursive = 0)
    {
        var mt = (MessageType)protocol.GetByte();
        switch (mt)
        {
            case MessageType.Init:
                Gamedata.Instance.ClinetId = protocol.GetByte();
                Debug.Log("Get " + Gamedata.Instance.ClinetId.ToString());
                break;
            case MessageType.RandomSeed:
                int seed = protocol.GetInt32();
                random = new Random((UInt64)seed);
                Debug.Log("random seed" + seed.ToString());
                 PlayerCreator.Instance.OnInit(this);
                break;
            case MessageType.Frame:
                int len = protocol.GetInt32();
                for (int i = 0;i < len;i++)
                {
                    if (protocol.GetByte() == 1)
                    {
                        protocol.GetVector2();
                    }
                }
                //protocol.GetVector2();
                Debug.Log("get frame " + len);
                break;
            case MessageType.End:
                Debug.Log("Get End");
                break;
            default:
                Debug.Log("error message type : " + mt.ToString());
                break;
        }

        if (mt != MessageType.End && recursive < 5)
            ParseMessage(protocol, recursive + 1);

        if (protocol.Length > 0)
        {
            ParseMessage(protocol, recursive + 1);
        }
            //Debug.Log("not parse: " + protocol.Length.ToString());

    }



    public enum MessageType : byte
    {
        Init = 1,
        RandomSeed = 2,
        Frame = 3,
        End = 255
    }
}
