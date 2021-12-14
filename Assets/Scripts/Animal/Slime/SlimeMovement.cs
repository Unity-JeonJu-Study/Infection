using System;
using UnityEngine;

[Serializable]
public class SlimeMovement : Movement
{
    [SerializeField, ReadOnly] private float jumpPowerInWater;
    public SlimeMovement(PlayerMovement playerMovement) : base(playerMovement)
    {
        jumpPowerInWater = playerMovement.jumpPower * 0.1f;
    }
    
    public override void Execute()
    {
        Move();
        sensor.CheckGround();
        Buoyancy();
        Jump(playerMovement.jumpPower);
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

    public override void Jump(float jumpPower)
    {
        if (playerMovement.canJump && playerMovement.isGround && playerInput.IsJumpKeyPressed())
        {
            base.Jump(jumpPower);
        }
        else if (playerMovement.isInWater && playerInput.IsJumpKeyPressed())
        {
            base.Jump(jumpPowerInWater);
        }
    }
    
    public void Buoyancy()
    {
        if (playerMovement.isInWater)
        {
            playerMovement._constantForce.force = new Vector3(0, 9f, 0);
        }
        else
        {
            playerMovement._constantForce.force = Vector3.zero;
        }
    }
}
