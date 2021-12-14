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
        sensor.CheckGround();
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

    private void AnimationRun(bool run)
    {
        playerMovement._animator.SetBool(RunHash, run);
    }
    
    public override void Jump()
    {
        if (!playerMovement.isInWater && playerMovement.canJump && playerMovement.isGround && playerInput.IsJumpKeyPressed())
        {
            base.Jump();
        }
    }

}