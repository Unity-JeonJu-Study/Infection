using System.Collections;
using UnityEngine;
using Sirenix.OdinInspector;


public class PressableObject : MonoBehaviour
{
    Collider curCollider;
    Animator animator;

    Vector3 startPos, endPos;

    private void Awake() {
        curCollider = GetComponent<Collider>();
        animator = GetComponent<Animator>();

        startPos = transform.position;
        endPos = new Vector3(startPos.x, startPos.y - 0.3f, startPos.z);
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
        StartCoroutine(MoveObject(startPos, endPos));
    }

    private IEnumerator MoveObject(Vector3 posA, Vector3 posB) {
        float curValue = 0f;
        while(curValue < 1) {
            transform.position = Vector3.Lerp(posA, posB, curValue);
            curValue += 0.01f;
            yield return null;
        }
    }

    [Button("Deactivate Event")]
    private void DeactivateEvent() {
        StartCoroutine(MoveObject(endPos, startPos));
    }
}