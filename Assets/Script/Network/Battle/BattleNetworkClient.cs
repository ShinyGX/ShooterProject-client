using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleNetworkClient : MonoBehaviour
{
    private BattleNetworkHandler client;

    void Awake()
    {
        client = new BattleNetworkHandler();
        client.Connect("127.0.0.1", 9963);


    }


    private void FixedUpdate()
    {
        if(client.MessageQue.Count > 0)
        {
            client.ParseMessage(client.MessageQue.Dequeue());
        }
    }

    private void OnDestroy()
    {
        client.Stop();
    }
}
