using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleNetworkClient : MonoBehaviour
{

    public static readonly Fixed deltaTime = new Fixed(0.066f);

    private BattleNetworkHandler client;

    private KeyboardInput keyboardInput;

    void Awake()
    {

        keyboardInput = FindObjectOfType<KeyboardInput>();

        client = new BattleNetworkHandler();
        client.Connect(Gamedata.Instance.Ip, Gamedata.Instance.Port);
    }


    private void FixedUpdate()
    {
        if(client.MessageQue.Count > 0)
        {
            client.ParseMessage(client.MessageQue.Dequeue());
        }

        CommitKey();
    }

    private void CommitKey()
    {
        if (client == null || client.gameLoop == null)
            return;

        client.gameLoop.SetMovement(keyboardInput.Direction);
    }

    private void OnDestroy()
    {
        client.Stop();
    }
}
