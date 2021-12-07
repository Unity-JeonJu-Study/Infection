using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player Movement Information")]
    public float movementSpeed;
    public float rotationSpeed;
    public float jumpPower;
    public bool isGround;
    public float rayDistance;
    public bool canJump;

    private Animator _animator;
    private PlayerInput playerInput;
    private Rigidbody _rigidbody;
    private static readonly int WalkHash = Animator.StringToHash("Walk");

    
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        playerInput = GetComponent<PlayerInput>();
        _rigidbody = GetComponent<Rigidbody>();
        canJump = true;
    }

    private void FixedUpdate()
    {
        Movement();
        CheckGround();
        Jump();
    }

    private void Movement()
    {
        float forward = playerInput.InputForward * movementSpeed;
        float side = playerInput.InputSide * movementSpeed;
        if (forward == 0 && side == 0)
        {
            AnimationMovement(false);
            return;
        }
        Vector3 dir = new Vector3(side, _rigidbody.velocity.y, forward);
        AnimationMovement(true);
        _rigidbody.velocity = dir;
        transform.rotation = Quaternion.LookRotation(new Vector3(playerInput.InputSide, 0, playerInput.InputForward));
    }

    private void AnimationMovement(bool walk)
    {
        _animator.SetBool(WalkHash, walk);
    }

    private void CheckGround()
    {
        if (Physics.Raycast(transform.position + new Vector3(0f, 0.2f, 0f), Vector3.down, rayDistance))
        {
            isGround = true;
        }
        else
        {
            isGround = false;
        }
    }

    private void Jump()
    {
        if (canJump && isGround && playerInput.IsJumpKeyPressed())
        {
            _rigidbody.AddForce(0, jumpPower, 0, ForceMode.Impulse);
            _animator.SetTrigger("Jump");
        }    
    }
}
