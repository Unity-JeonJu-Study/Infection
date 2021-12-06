using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdViewCamera : MonoBehaviour
{
    [Header("Camera Information")]
    public float offsetX;
    public float offsetY;
    public float offsetZ;
    public float cameraSpeed;

    [Header("Target Object")] 
    public GameObject target;

    private Vector3 originPosition;
    private Vector3 targetPosition;
    private void Update()
    {
        originPosition = transform.position;
        targetPosition = target.transform.position;
        Vector3 changePosition = new Vector3(
            targetPosition.x + offsetX,
            targetPosition.y + offsetY,
            targetPosition.z + offsetZ);
        transform.position = Vector3.Slerp(originPosition, changePosition, cameraSpeed * Time.deltaTime);
    }
}
