using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerType
{
    Slime,
    Chick,
}

public class PlayerMovement : MonoBehaviour
{
    [Header("Player Movement Information")]
    public float movementSpeed;
    public float rotationSpeed;
    public float jumpPower;
    public bool isGround;
    public float rayDistance;
    public bool canJump;
    public Movement movement;
    [HideInInspector] public AnimalData animalData;
    public Animator _animator;
    public PlayerInput playerInput;
    public Rigidbody _rigidbody;
    private GameObject currentAnimal;
    private PlayerInfect playerInfect;

    private void Start()
    {
        currentAnimal = playerInfect.currentAnimal;
        _animator = currentAnimal.GetComponent<Animator>();
        playerInput = currentAnimal.GetComponent<PlayerInput>();
        _rigidbody = currentAnimal.GetComponent<Rigidbody>();
        canJump = true;
        InitMoveInformation("Slime");
        movement = new SlimeMovement(this);
    }

    private void FixedUpdate()
    {
        movement.Excute();
    }

    public void ChangeStatus(string animalName)
    {
        switch (animalName)
        {
            case "Slime":
                InitMoveInformation(animalName);
                movement = new SlimeMovement(this);
                break;
            case "Chick":
                InitMoveInformation(animalName);
                movement = new ChickMovement(this);
                break;
        }
    }

    public void InitMoveInformation(string animalName)
    {
        animalData = Resources.Load<AnimalData>("Data/Animal/"+animalName);
        movementSpeed = animalData.movementSpeed;
        rotationSpeed = animalData.rotationSpeed;
        jumpPower = animalData.jumpPower;
        rayDistance = animalData.rayDistance;
        canJump = true;
    }
    
    
}
