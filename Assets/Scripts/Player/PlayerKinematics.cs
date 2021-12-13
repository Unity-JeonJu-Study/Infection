using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKinematics : MonoBehaviour
{
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
        Debug.Log("밀 수 있다!!");
        sensor.hitForward.collider.transform.position += transform.forward * Time.deltaTime;
    }
}
