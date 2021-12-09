using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sensor : MonoBehaviour
{
    public float interactRayDistance;
    public RaycastHit hit;

    private PlayerMovement playerMovement;
    private void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    public bool CheckForward()
    {
        interactRayDistance = playerMovement.interactRayDistance;
        bool cast = Physics.BoxCast(transform.position, transform.lossyScale / 2.0f, transform.forward * 0.8f,
            out hit,
            transform.rotation, interactRayDistance, LayerMask.GetMask("Animal"));

        return cast;
    }
}
