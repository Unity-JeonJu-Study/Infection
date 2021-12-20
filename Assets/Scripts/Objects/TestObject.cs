using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// this script exists for test purpose only

public class TestObject : MonoBehaviour
{
    Rigidbody curRigidbody;

    private void Awake() {
        curRigidbody = GetComponent<Rigidbody>();
    }

    private void Update() {
        curRigidbody.position += transform.forward * Input.GetAxisRaw("Horizontal") * -5f * Time.deltaTime;
        curRigidbody.position += transform.right * Input.GetAxisRaw("Vertical") * 5f * Time.deltaTime;

        if(Input.GetKeyDown(KeyCode.Space))
            curRigidbody.AddForce(Vector3.up * 10f, ForceMode.Impulse);
    }
}