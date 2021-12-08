using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeMovement : Movement
{
    public override void Excute()
    {
        Move();
        CheckGround();
        Jump();
    }

    protected void CheckGround()
    {
        if (Physics.Raycast(playerMovement.transform.position +
                            new Vector3(0f,
                                0.2f,
                                0f),
            Vector3.down,
             playerMovement.rayDistance))
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
    public SlimeMovement(PlayerMovement playerMovement) : base(playerMovement)
    {
        
    }
}
