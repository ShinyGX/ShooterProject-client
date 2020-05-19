using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleNetworkClient : MonoBehaviour
{

    public static readonly Fixed deltaTime = new Fixed(0.066f);

    private BattleNetworkHandler handler;

    private KeyboardInput keyboardInput;

    void Awake()
    {
        keyboardInput = FindObjectOfType<KeyboardInput>();


        handler = new BattleNetworkHandler();
   
        handler.Connect("127.0.0.1",9999);
    }


    private void FixedUpdate()
    {
        if(handler.MessageQue.Count > 0)
        {
            handler.ParseMessage(handler.MessageQue.Dequeue());
        }

        CommitKey();
    }

    private void CommitKey()
    {
        if (handler == null || handler.gameLoop == null)
            return;

        handler.gameLoop.SetMovement(keyboardInput.Direction);
    }

    private void OnDestroy()
    {
        handler.Stop();
    }
}
