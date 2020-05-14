using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCreator : MonoSignleton<PlayerCreator>, INetGameManager
{
    public int player = 2;
    public int Layer { get { return 99; } }

    public void OnInit(BattleNetworkHandler handler)
    {

        var playerPos = new Fixed2(handler.random.Range(0, 20), handler.random.Range(0, 20));
        for (int i = 0; i < player; i++)
        {
            var netObj = new NetGameObject();
            netObj.Init(handler, i);
            netObj.prefab = "Player1";

            var localPos = playerPos + Fixed2.left * i.ToFixed();
            netObj.transform.position =
                new Fixed3(localPos.X, Fixed.zero, localPos.Y);

            netObj.InstantiateGameObject();
        }
    }
}
