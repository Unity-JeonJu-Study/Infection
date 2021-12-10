using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sensor : MonoBehaviour
{
    public float groundNormal;
    public float groundDistance;
    public float groundRayDistance;
    public float interactRayDistance;
    public RaycastHit hit;
    
    private PlayerMovement playerMovement;
    private void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    public bool CheckForward()
    {
        bool cast = Physics.BoxCast(transform.position, transform.lossyScale / 2.0f, transform.forward * 0.8f,
                                    out hit,
                                    transform.rotation, interactRayDistance, LayerMask.GetMask("Animal"));

        return cast;
    }

    public void CheckGround()
    {
        
    }
}
