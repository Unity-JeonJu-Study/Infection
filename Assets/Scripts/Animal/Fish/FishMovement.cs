using System;
using UnityEngine;

[Serializable]
public class FishMovement : Movement
{
    public FishMovement(PlayerMovement playerMovement) : base(playerMovement)
    {
        
    }
    
    public override void Execute()
    {
        Move();
        sensor.CheckGround();
        Jump();
        Buoyancy();
    }

    public override void Move()
    {
        if (playerInput.InputForward == 0 && playerInput.InputSide == 0)
        {
            AnimationWalk(false);
            return;
        }
        base.Move();
        AnimationWalk(true);
    }

    public override void Jump()
    {
        if (playerMovement.isInWater && playerInput.IsJumpKeyPressed())
        {
            base.Jump();
        }
    }

    public void Buoyancy()
    {
        if (playerMovement.isInWater)
        {
            playerMovement._constantForce.force = new Vector3(0, 9f, 0);
            Debug.Log("나 부력있다!");
        }    
    }
}