using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetInputUnit
{

    public Fixed2 Direction
    {
        get;
        protected set;
    }



    public void ReceviceStep(INetworkProtocol protocol)
    {
        Direction = protocol.GetVector2();
    }
}
