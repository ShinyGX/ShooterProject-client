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


    private bool CanProcessInput()
    {
        return true;
    }

    public float GetLookInputHorizontal()
    {
        return 0f;
    }

    public float GetLookInputVertical()
    {
        return 0f;
    }

    public bool GetJumpInput()
    {
        return false;
    }

    public Vector3 GetMoveInput()
    {
        if(CanProcessInput())
        {
            Vector3 move = new Vector3(Direction.x.ToFloat(), 0f, Direction.y.ToFloat());

            move = Vector3.ClampMagnitude(move, 1);
            return move;
        }

        return Vector3.zero;
    }
}
