using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCommandDispatch : MonoBehaviour
{
    public GameCommandType type;
    public List<GameCommandReceiver>  interatorObjList;

    public bool sendOnce = false;

    private bool hasSended = false;
    private float lastSendTime;

    public void Send(object sender)
    {
        if (sendOnce && hasSended)
            return;

        hasSended = true;
        lastSendTime = Time.time;

        foreach (var obj in interatorObjList)
        {
            obj.Receive(type, sender);
        }

    }
}
