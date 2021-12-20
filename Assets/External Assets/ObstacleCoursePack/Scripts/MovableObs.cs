using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableObs : MonoBehaviour
{
	public float distance = 5f; //Distance that moves the object
	public bool horizontal = true; //If the movement is horizontal or vertical
	public float speed = 3f;
	public float offset = 0f; //If yo want to modify the position at the start 
	public bool isOnPlayer;

	private bool isForward = true; //If the movement is out
	public Vector3 startPos;
	private Transform playerTransform;
   
    void Awake()
    {
		startPos = transform.position;
		if (horizontal)
			transform.position += Vector3.right * offset;
		else
			transform.position += Vector3.forward * offset;
	}

    // Update is called once per frame
    void Update()
    {
		Move(transform);
		// if (isOnPlayer)
		// {
		// 	Move(playerTransform);
		// }
    }

	private void OnTriggerEnter(Collider other) {
		if (other.CompareTag("Player"))
		{
			playerTransform = other.transform;
			isOnPlayer = true;
			playerTransform.gameObject.transform.parent.transform.SetParent(this.gameObject.transform);
		}
	}

	private void OnTriggerExit(Collider other) {
		if (other.CompareTag("Player"))
		{
			playerTransform.gameObject.transform.parent.transform.SetParent(null);
			isOnPlayer = false;
		}
	}

	private void Move(Transform origin)
	{
		if (horizontal)
		{
			if (isForward)
			{
				if (origin.position.x < startPos.x + distance)
				{
					origin.position += Vector3.right * Time.deltaTime * speed;
				}
				else
					isForward = false;
			}
			else
			{
				if (origin.position.x > startPos.x)
				{
					origin.position -= Vector3.right * Time.deltaTime * speed;
				}
				else
					isForward = true;
			}
		}
		else
		{
			if (isForward)
			{
				if (origin.position.z < startPos.z + distance)
				{
					origin.position += Vector3.forward * Time.deltaTime * speed;
				}
				else
					isForward = false;
			}
			else
			{
				if (origin.position.z > startPos.z)
				{
					origin.position -= Vector3.forward * Time.deltaTime * speed;
				}
				else
					isForward = true;
			}
		}
	}
}
