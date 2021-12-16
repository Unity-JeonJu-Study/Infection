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
        
        // 수정본
        if(playerInput.InputForward >0)
            playerMovement.transform.forward = moveDir.normalized;  
        _rigidbody.velocity = moveDir * playerMovement.movementSpeed;
        
        //_rigidbody.velocity = moveDir * playerMovement.movementSpeed;
        //playerMovement.transform.forward = moveDir;
        //Camera.main.transform.rotation = Quaternion.Euler(moveDir);
        //Camera.main.transform.forward = moveDir;
        // if(moveDir.z > 0)
        //     playerMovement.transform.forward = moveDir;
        // else if(moveDir.z <0)
        //     playerMovement.transform.forward = -moveDir;
        // float forward = playerInput.InputForward * playerMovement.movementSpeed;
        // float side = playerInput.InputSide * playerMovement.movementSpeed;
        // if (!playerMovement.isGround && sensor.CheckForward())
        // {    
        //     forward = 0;
        //     side = 0;
        // }



        // _rigidbody.velocity = new Vector3(side, 0f, forward);
        // _rigidbody.transform.forward = new Vector3(side, 0f, forward);
        //
        //
        // Camera.main.transform.forward = moveDir.normalized;
        // Debug.Log("카메라 y : " + cameraRotation);
        // Vector3 dir = Vector3.zero;
        // if (225 <= cameraRotation && cameraRotation <= 315)
        // {   
        //     Debug.Log("방향 바뀜");
        //     dir = new Vector3(-forward, _rigidbody.velocity.y, side);
        //     newRotation = Quaternion.LookRotation( new Vector3(playerInput.InputForward * playerMovement.rotationSpeed,
        //         0f,
        //         playerInput.InputSide * playerMovement.rotationSpeed));
        // }
        // if (45 <= cameraRotation && cameraRotation <= 135)
        // {
        //     dir = new Vector3(forward, _rigidbody.velocity.y, side);
        //     newRotation = Quaternion.LookRotation( new Vector3(playerInput.InputForward * playerMovement.rotationSpeed,
        //         0f,
        //         playerInput.InputSide * playerMovement.rotationSpeed));
        // }
        // if (135 <= cameraRotation && cameraRotation <= 225)
        // {
        //     dir = new Vector3(side, _rigidbody.velocity.y, -forward);
        //     newRotation = Quaternion.LookRotation( new Vector3(playerInput.InputSide * playerMovement.rotationSpeed,
        //         0f,
        //         playerInput.InputForward * playerMovement.rotationSpeed));
        // }
        // if ((0 <= cameraRotation && cameraRotation <= 45) || cameraRotation >= 315)
        // {
        //     dir = new Vector3(side, _rigidbody.velocity.y, forward);
             // newRotation = Quaternion.LookRotation( new Vector3(playerInput.InputSide * playerMovement.rotationSpeed,
             //     0f,
             //     playerInput.InputForward * playerMovement.rotationSpeed)).normalized;
        // }
       // _rigidbody.velocity = dir;
       //playerMovement.transform.forward = moveDir;
       //playerMovement.transform.rotation = Quaternion.Lerp(_rigidbody.rotation, newRotation, 0.1f);
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
