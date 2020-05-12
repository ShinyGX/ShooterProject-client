using System;
using System.Collections.Generic;
using UnityEngine;

public class GameCommandReceiver : MonoBehaviour
{
    private Dictionary<GameCommandType, List<Action<object>>> handerDic = new Dictionary<GameCommandType, List<Action<object>>>();


    public void Receive(GameCommandType type,object sender)
    {
        List<Action<object>> callbackList;
        if(handerDic.TryGetValue(type,out callbackList))
        {
            foreach(var callback in callbackList)
            {
                callback(sender);
            }
        }
    }

    public void Register(GameCommandType type,ICommandHandler handler)
    {
        List<Action<object>> callbackList;
        if(!handerDic.TryGetValue(type,out callbackList))
        {
            handerDic[type] = new List<Action<object>>();
            callbackList = handerDic[type];
        }

        callbackList.Add(handler.OnReceve);
    }

    public void Remove(GameCommandType type,ICommandHandler handler)
    {
        handerDic[type].Remove(handler.OnReceve);
    }
   
}
