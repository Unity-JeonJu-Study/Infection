using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeResueTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            InGameUIManager.instance.AddOneRescuedSlimeSlot();
        }
    }
}
