using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKinematics : MonoBehaviour
{
    public float power;
    
    private Sensor sensor;

    private void Awake()
    {
        sensor = GetComponent<Sensor>();
    }

    private void Update()
    {
        Push();
    }

    public bool CanPush()
    {
        if (sensor.CheckForward() && sensor.hitForward.collider.gameObject.layer.Equals(LayerMask.NameToLayer("Movable")))
            return true;
        return false;
    }

    public void Push()
    {
        if (!CanPush())
            return;
        sensor.hitForward.collider.GetComponent<Rigidbody>().AddForce(transform.forward * power);
    }
}
