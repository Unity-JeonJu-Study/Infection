using System;
using UnityEngine;

[Serializable]
public class FishMovement : Movement
{
    private static readonly int RunHash = Animator.StringToHash("Run");

    public FishMovement(PlayerMovement playerMovement) : base(playerMovement)
    {
        
    }
    
    public override void Execute()
    {
        Move();
        sensor.CheckGround();
        Jump(playerMovement.jumpPower);
        Buoyancy();
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

    public override void Jump(float jumpPower)
    {
        if (playerMovement.isInWater && playerInput.IsJumpKeyPressed())
        {
            base.Jump(jumpPower);
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
    
    private void AnimationRun(bool run)
    {
        playerMovement._animator.SetBool(RunHash, run);
    }
}