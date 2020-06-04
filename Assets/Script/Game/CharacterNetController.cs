using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterNetController : NetGameObject
{
    private NetInputUnit input;

    //人物移动
    private float maxSpeedOnGround = 10f;
    private float speedModify = 1f;
    private float movementSharpnessOnGround = 15;
    private float maxSpeedInAir = 10f;
    private float accelerationSpeedInAir = 25f;
    private float gravity = 20f;
    private float jumpForce = 9f;

    public Vector3 lastImpactSpeed;

    // 判断是否在地面
    private float groundCheckDistance = 0.05f;
    private float groundCheckDistanceInAir = 0.07f;
    private int groundCheckLayer = LayerMask.NameToLayer("Ground");


    private bool isOnGround;

    private float lastJumpTime = 0f;
    private float jumpCD = 0.2f;

    private Vector3 characterVelocity = Vector3.zero;
    private Vector3 groundNormal = Vector3.up;

    private CharacterController controller;

    public override void Init(BattleNetworkHandler handler, int clientId = -1)
    {
        base.Init(handler, clientId);

        input = handler.gameLoop[clientId];

       
    }

    public override void InitAfterCreateViewObject()
    {
        base.InitAfterCreateViewObject();
        controller = logicObject.GetComponent<CharacterController>();
    }


    public override void Update()
    {

        bool wasGrounded = isOnGround;
        GroundCheck();

  
        HandlerCharacterMovement();
    }

    private void HandlerCharacterMovement()
    {
        Vector3 worldspaceMoveInput = logicObject.transform.TransformVector(input.GetMoveInput());

        if (isOnGround)
        {
            Vector3 targetVelocity = maxSpeedOnGround * worldspaceMoveInput * speedModify;
            targetVelocity = GetDirectionReorientedOnSlope(targetVelocity.normalized, groundNormal) * targetVelocity.magnitude;
            characterVelocity = Vector3.Lerp(characterVelocity, targetVelocity, movementSharpnessOnGround * BattleApplicationBooter.DeltaTime);

            //jump
            if(isOnGround && input.GetJumpInput())
            {
                characterVelocity = new Vector3(characterVelocity.x, 0f, characterVelocity.z);

                characterVelocity += Vector3.up * jumpForce;

                lastJumpTime = BattleGameLoop.Time;


                isOnGround = false;
                groundNormal = Vector3.up;
            }

        }
        //in air
        else
        {
            characterVelocity += worldspaceMoveInput * accelerationSpeedInAir * BattleApplicationBooter.DeltaTime;

            float verticalVelocity = characterVelocity.y;
            Vector3 horizontalVelocity = Vector3.ProjectOnPlane(characterVelocity, Vector3.up);

            horizontalVelocity = Vector3.ClampMagnitude(horizontalVelocity, maxSpeedInAir);
            characterVelocity = horizontalVelocity + (Vector3.up * verticalVelocity);

            characterVelocity += Vector3.down * gravity * BattleApplicationBooter.DeltaTime;

        }


        Vector3 bottomBeforeMove = GetCharacterBottomHemisphere();
        Vector3 topBeforeMove = GetCharacterTopHemisphere();

        controller.Move(characterVelocity * BattleApplicationBooter.DeltaTime);

        lastImpactSpeed = Vector3.zero;
        if(Physics.CapsuleCast(bottomBeforeMove,
                               topBeforeMove,
                               controller.radius,
                               characterVelocity.normalized,
                               out RaycastHit hit,
                               characterVelocity.magnitude * BattleApplicationBooter.DeltaTime,
                               -1,
                               QueryTriggerInteraction.Ignore))
        {
            lastImpactSpeed = characterVelocity;
            characterVelocity = Vector3.ProjectOnPlane(characterVelocity, hit.normal);
        }


    }

    private void GroundCheck()
    {
        float groundCheckDistance = isOnGround ? (controller.skinWidth + this.groundCheckDistance) : this.groundCheckDistanceInAir;

        isOnGround = false;
        groundNormal = Vector3.up;
        if(BattleGameLoop.Time >= (lastJumpTime + jumpCD))
        {

            if (Physics.CapsuleCast(GetCharacterBottomHemisphere(),
                                    GetCharacterTopHemisphere(),
                                    controller.radius,
                                    -Vector3.up,
                                    out RaycastHit hit,
                                    1.2f,
                                    Physics.AllLayers,
                                    QueryTriggerInteraction.Ignore))
            {
  
                groundNormal = hit.normal;

                //如果法线方向和人物上方向同向，且坡度小于CharacterController能接受的坡度，就认为已经到地上了
                if(Vector3.Dot(hit.normal,logicObject.transform.up) > 0f &&
                    IsUnderSlopeLimit(groundNormal))
                {
                    isOnGround = true;
                    if(hit.distance > controller.skinWidth)
                    {
                        controller.Move(Vector3.down * hit.distance);
                    }
                }
            }
        }
    }

    private Vector3 GetDirectionReorientedOnSlope(Vector3 dir,Vector3 slopeNormal)
    {
        Vector3 dirRight = Vector3.Cross(dir, logicObject.transform.up);
        return Vector3.Cross(slopeNormal, dirRight).normalized;
    }


    private Vector3 GetCharacterBottomHemisphere()
    {
        return logicObject.transform.position + (logicObject.transform.up * controller.radius);
    }

    private Vector3 GetCharacterTopHemisphere()
    {
        return logicObject.transform.position + (logicObject.transform.up * (controller.height - controller.radius));
    }

    private bool IsUnderSlopeLimit(Vector3 normal)
    {
        return Vector3.Angle(logicObject.transform.up, normal) <= controller.slopeLimit;
    }
}
