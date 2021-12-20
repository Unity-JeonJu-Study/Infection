using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.Utilities.Editor;
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
        var cameraTransform = Camera.main.transform;
        var cameraRotation = cameraTransform.rotation.eulerAngles.y;
        var cameraForward = cameraTransform.rotation * Vector3.forward;
        var cameraRight = cameraTransform.rotation * Vector3.right;
        
        var lookForward = new Vector3(cameraForward.x, 0f, cameraForward.z).normalized;
        var lookRight = new Vector3(cameraRight.x, 0f, cameraRight.z).normalized;
        moveDir = lookForward * playerInput.InputForward + lookRight * playerInput.InputSide;
        
        if(playerInput.InputForward > 0)
            playerMovement.transform.forward = moveDir.normalized;
        _rigidbody.velocity = moveDir * playerMovement.movementSpeed + new Vector3(0, playerMovement._rigidbody.velocity.y, 0);

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
