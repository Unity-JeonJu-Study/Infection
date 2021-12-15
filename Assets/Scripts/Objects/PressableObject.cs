using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;


public class PressableObject : MonoBehaviour
{
    Collider curCollider;
    Animator animator;

    private void Awake() {
        curCollider = GetComponent<Collider>();
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other) {
        switch(other.tag) {
            case "HeavyObject":
            case "Player": 
            ActivateEvent();
            break;
        }
    }

    private void OnTriggerExit(Collider other) {
        switch(other.tag) {
            case "HeavyObject":
            case "Player": 
            DeactivateEvent();
            break;
        }
    }

    [Button("Activate Event")]
    private void ActivateEvent() {
        Debug.Log("activate event");
    }

    [Button("Deactivate Event")]
    private void DeactivateEvent() {
        Debug.Log("deactivate event");
    }
}
