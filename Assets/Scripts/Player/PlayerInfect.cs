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

    private PlayerInput playerInput;
    private Vector3 rayOriginPosition;
    private SkinnedMeshRenderer meshRenderer;

    private void Awake()
    {
        meshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
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
            hit.collider.GetComponent<NavMeshAgent>().enabled = false;
            hit.collider.GetComponent<CharacterController>().enabled = false;
            hit.collider.GetComponent<Animal_WanderScript>().enabled = false;

            switch (hit.collider.name)
            {
                case "Chick":
                    hit.collider.gameObject.AddComponent<ChickMovement>();
                    break;
                default:
                    break;
            }
        }
    }
}
