using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public abstract class Movement
{
    protected PlayerMovement playerMovement;
    protected ConstantForce _constantForce;
    protected PlayerInput playerInput;
    protected Rigidbody _rigidbody;
    protected Animator _animator;
    protected Sensor sensor;
    
    private static readonly int WalkHash = Animator.StringToHash("Walk");
    private static readonly int JumpHash = Animator.StringToHash("Jump");

    protected Movement(PlayerMovement playerMovement)
    {
        this.playerMovement = playerMovement;
        playerInput = playerMovement.playerInput;
        _rigidbody = playerMovement._rigidbody;
        _animator = playerMovement._animator;
        sensor = playerMovement.sensor;
        _constantForce = playerMovement._constantForce;
    }
    
    public abstract void Execute();

    public virtual void Move()
    {
        
        float forward = playerInput.InputForward * playerMovement.movementSpeed;
        float side = playerInput.InputSide * playerMovement.movementSpeed;
        if (!playerMovement.isGround && sensor.CheckForward())
        {
            forward = 0;
            side = 0;
        }
        Vector3 dir = new Vector3(side, _rigidbody.velocity.y, forward);
        _rigidbody.velocity = dir;
        
        playerMovement.transform.rotation = Quaternion.LookRotation( new Vector3(playerInput.InputSide * playerMovement.rotationSpeed,
            0f,
            playerInput.InputForward * playerMovement.rotationSpeed));
    }

    protected virtual void AnimationWalk(bool walk)
    {
        playerMovement._animator.SetBool(WalkHash, walk);
    }

    public virtual void Jump(float jumpPower)
    {
        Debug.Log(jumpPower + "점프파워");
        _rigidbody.AddForce(0,  jumpPower, 0, ForceMode.Impulse);
        _animator.SetBool(JumpHash, true);
    }

 
}
