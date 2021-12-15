using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalPoint : MonoBehaviour
{
    public GameStage stage;
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
            GameManager.Instance.UpdateStage(stage);
    }
}
