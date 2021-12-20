using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Item
{
    MasterKey,
    Slime
}

public class Player : MonoBehaviour
{
    public List<Item> inventory;
    [HideInInspector] public Sensor sensor;
    [HideInInspector] public PlayerKinematics playerKinematics;
    [HideInInspector] public PlayerMovement playerMovement;
    [HideInInspector] public AnimalData animalData;


    private void Awake()
    {
        sensor = GetComponent<Sensor>();
        playerKinematics = GetComponent<PlayerKinematics>();
        playerMovement = GetComponent<PlayerMovement>();
    }


    public void InitMoveInformation(string animalName)
    {
        animalData = Resources.Load<AnimalData>("Data/Animal/" + animalName);
        playerMovement.movementSpeed = animalData.movementSpeed;
        playerMovement.rotationSpeed = animalData.rotationSpeed;
        playerMovement.jumpPower = animalData.jumpPower;
        sensor.groundRayDistance = animalData.groundRayDistance;
        sensor.interactRayDistance = animalData.interactRayDistance;
        playerKinematics.power = animalData.power;
        playerMovement.canJump = true;
    }

    public void LoadCheckPoint()
    {
        transform.position = GameManager.Instance.SpawnPoint.transform.position;
    }
}
