using System;
using System.Collections.Generic;
using UnityEngine;

public class BattleNetworkHandler : NetworkHandler
{

    public BattleGameLoop gameLoop;

    public Random random;

    protected override void Init()
    {
        gameLoop = new BattleGameLoop(this);
    }

    //private void AddNetGameManager(INetGameManager[] managers)
    //{
    //    netGameManagerList.AddRange(managers);
    //    netGameManagerList.Sort((a, b) =>
    //    {
    //        return a.Layer > b.Layer ? 1 : -1;
    //    });
    //}

    public void ParseMessage(INetworkProtocol protocol, int recursive = 0)
    {
        var mt = (MessageType)protocol.GetByte();
        switch (mt)
        {
            case MessageType.Init:
                Debug.Log("Get");
                random = new Random((UInt64)protocol.GetInt64());

                PlayerCreator.Instance.OnInit(this);


                break;
            case MessageType.Frame:

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



    public enum MessageType : byte
    {
        Init = 1,
        Frame = 2,
        End = 255
    }
}
