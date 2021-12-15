using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    private Sensor sensor;

    private void Start()
    {
        sensor = GetComponent<Sensor>();
    }

    private void Update()
    {
        OpenDoor();
    }

    public void OpenDoor()
    {
        if (sensor.CheckForward() && sensor.hitForward.collider.gameObject.layer == LayerMask.NameToLayer("Door"))
        {
            sensor.hitForward.collider.GetComponent<Animator>().Play("OpenDoor02");
        }
    }
}
