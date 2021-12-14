using System;

[Serializable]
public class SlimeMovement : Movement
{
    
    public SlimeMovement(PlayerMovement playerMovement) : base(playerMovement)
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
        base.Move();
        AnimationWalk(true);
    }

    
    
    public override void Jump()
    {
        if (playerMovement.isInWater || (playerMovement.canJump && playerMovement.isGround && playerInput.IsJumpKeyPressed()))
        {
            base.Jump();
        }
    }
}
