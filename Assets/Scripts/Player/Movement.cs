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
    protected Quaternion cameraOriginRot;
    protected Transform cameraTransform;
    protected Rigidbody _rigidbody;
    protected Animator _animator;
    protected Vector3 moveDir;
    protected Sensor sensor;

    private Quaternion newRotation;
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
        cameraOriginRot = Quaternion.identity;
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
        newRotation = Quaternion.LookRotation( new Vector3(playerInput.InputSide * playerMovement.rotationSpeed,
            0f,
            playerInput.InputForward * playerMovement.rotationSpeed));
        playerMovement.transform.rotation = Quaternion.Lerp(_rigidbody.rotation, newRotation, 0.01f);
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
