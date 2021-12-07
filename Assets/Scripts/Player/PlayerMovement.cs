using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player Movement Information")]
    public float movementSpeed;
    public float rotationSpeed;
    public bool isGround;

    private Animator _animator;
    private PlayerInput playerInput;
    private Rigidbody _rigidbody;
    private static readonly int WalkHash = Animator.StringToHash("Walk");

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        playerInput = GetComponent<PlayerInput>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Movement();
        CheckGround();
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
        Vector3 dir = new Vector3(side, 0, forward);
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
        

    }

    private void OnDrawGizmos()
    {
        RaycastHit hit;
        // Physics.Raycast (레이저를 발사할 위치, 발사 방향, 충돌 결과, 최대 거리)
        bool isHit = Physics.Raycast (transform.position, -transform.up, out hit, 0);

        Gizmos.color = Color.red;
        if (isHit) {
            Gizmos.DrawRay (transform.position, -transform.up * hit.distance);
            isGround = true;
        } else {
            Gizmos.DrawRay (transform.position, -transform.up * 0);
            isGround = false;
        }
        
        // Gizmos.color = Color.black;
        // if (Physics.BoxCast(transform.position, transform.lossyScale / 2.0f,
        //     -transform.up, out RaycastHit hit, transform.rotation, transform.lossyScale.y / 2.0f))
        // {
        //     Gizmos.DrawRay(transform.position, -transform.up * hit.distance);
        //     Gizmos.DrawWireCube(transform.position + -transform.up * hit.distance, transform.lossyScale);
        //     isGround = true;
        // }
        // else
        // {
        //     isGround = false;
        // }
        //Gizmos.DrawWireCube(transform.position, transform.lossyScale / 2.0f);

    }

    private void Jump()
    {
        
    }
}
