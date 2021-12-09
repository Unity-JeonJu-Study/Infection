using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SlimeMovement : Movement
{
    
    public SlimeMovement(PlayerMovement playerMovement) : base(playerMovement)
    {
        
    }
    
    public override void Execute()
    {
        Move();
        CheckGround();
        Jump();
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

    protected void CheckGround()
    {
        if (Physics.Raycast(playerMovement.transform.position +
                            new Vector3(0f,
                                0.2f,
                                0f),
            Vector3.down,
             playerMovement.groundRayDistance))
        {
            playerMovement.isGround = true;
        }
        else
        {
            playerMovement.isGround = false;
        }
    }
    
    public override void Jump()
    {
        if (playerMovement.canJump && playerMovement.isGround && playerInput.IsJumpKeyPressed())
        {
            base.Jump();
        }    
    }
}
