using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNetObject : NetGameObject
{
    private NetInputUnit input;

    public override void Init(BattleNetworkHandler handler, int clientId = -1)
    {
        base.Init(handler, clientId);

        input = handler.gameLoop[clientId];
    }


    public override void Update()
    {

        this.transform.position = 
            FixedMath.Lerp(
                this.transform.position, 
                new Fixed3(this.transform.position.x + input.Direction.x, this.transform.position.y, this.transform.position.z + input.Direction.y), 
                new Fixed(0.75f));

    }

   

}
