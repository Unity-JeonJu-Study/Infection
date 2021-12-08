using System;
using System.Collections;
using System.Collections.Generic;
using PolyPerfect;
using UnityEngine;
using UnityEngine.AI;

public class PlayerInfect : MonoBehaviour
{
    [Header("Infection Information")] 
    public float rayDistance;
    public Dictionary<string, GameObject> animals;
    public GameObject currentAnimal;

    private PlayerMovement playerMovement;
    private PlayerInput playerInput;
    private Vector3 rayOriginPosition;
    private SkinnedMeshRenderer meshRenderer;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        playerMovement = GetComponent<PlayerMovement>();
        animals = new Dictionary<string, GameObject>();
        InitAnimals();
        currentAnimal = animals["Slime"];
        meshRenderer = currentAnimal.GetComponentInChildren<SkinnedMeshRenderer>();
    }

    private void InitAnimals()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject animal = transform.GetChild(i).gameObject;
            Debug.Log(animal.name);
            animals.Add(animal.name, animal);
        }
    }

    private void OnDrawGizmos()
    {
        meshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();

        rayOriginPosition =  meshRenderer.bounds.center;
        if (Physics.Raycast(rayOriginPosition, Vector3.forward, out RaycastHit hit, rayDistance))
        {
            Debug.DrawRay(rayOriginPosition, Vector3.forward, Color.red, rayDistance);
        }
        else
        {
            Debug.DrawRay(rayOriginPosition, Vector3.forward, Color.red, rayDistance);
        }
    }

    private void Update()
    {
        Infect();
    }

    private void Infect()
    {
        if (!playerInput.InfectKeyPressed)
            return;
        if (Physics.Raycast(rayOriginPosition, Vector3.forward, out RaycastHit hit, rayDistance, LayerMask.GetMask("Animals")))
        {
            currentAnimal.SetActive(false);
            currentAnimal =  animals[hit.collider.name.Split(' ')[0]];
            currentAnimal.SetActive(true);
            currentAnimal.transform.position = hit.collider.transform.position;
            playerMovement.ChangeStatus(currentAnimal.name);
            meshRenderer = currentAnimal.GetComponentInChildren<SkinnedMeshRenderer>();
        }
    }
}
