using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickMovement : Movement
{
    private static readonly int RunHash = Animator.StringToHash("Run");

    public ChickMovement(PlayerMovement playerMovement) : base(playerMovement)
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
        if (Input.GetKey(KeyCode.LeftShift))
        {
            playerInput.InputForward *= 1.2f;
            AnimationWalk(false);
            AnimationRun(true);
        }
        else
        {
            AnimationRun(false);
            AnimationWalk(true);
        }
        base.Move();
    }

    protected void CheckGround()
    {
        if (Physics.Raycast(playerMovement.transform.position +
                            new Vector3(0f,
                                0.2f,
                                0f),
            Vector3.down,
            sensor.groundRayDistance))
        {
            playerMovement.isGround = true;
        }
        else
        {
            playerMovement.isGround = false;
        }
    }

    public void AnimationRun(bool run)
    {
        playerMovement._animator.SetBool(RunHash, run);
    }
    
    public override void Jump()
    {
        if (playerMovement.canJump && playerMovement.isGround && playerInput.IsJumpKeyPressed())
        {
            base.Jump();
        }    
    }

}