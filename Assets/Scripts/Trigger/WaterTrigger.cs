using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log(other.transform.parent.name);
            other.transform.parent.GetComponent<PlayerMovement>().isInWater = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.parent.GetComponent<PlayerMovement>().isInWater = false;
        }
    }
}
