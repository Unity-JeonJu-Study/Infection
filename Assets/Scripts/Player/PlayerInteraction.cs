using System;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    private Vector3 distance;
    private Sensor sensor;
    private PlayerInput playerInput;
    private Transform platformTransform;
    private PlayerMovement playerMovement;

    private void Start()
    {
        sensor = GetComponent<Sensor>();
        playerInput = GetComponent<PlayerInput>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        OpenDoor();
        // PlatformMove();
    }

    public void OpenDoor()
    {
        if (sensor.CheckForward() && sensor.hitForward.collider.gameObject.layer == LayerMask.NameToLayer($"Door"))
        {
            sensor.hitForward.collider.GetComponent<Animator>().Play("OpenDoor02");
        }
    }
    //
    // public void PlatformMove()
    // {
    //     if (platformTransform == null)
    //         return;
    //     if (playerMovement.isGround && playerInput.InputForward + playerInput.InputSide == 0)
    //     {
    //         transform.position = platformTransform.position - distance;
    //     }
    // }
    //
    // private void OnTriggerStay(Collider other)
    // {
    //     if (other.CompareTag($"Platform"))
    //     {
    //         platformTransform = other.transform;
    //         distance = platformTransform.position - transform.position;
    //     }
    // }
    //
    // private void OnTriggerExit(Collider other)
    // {
    //     platformTransform = null;
    //
    // }
    
}
