using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KillZone : MonoBehaviour
{
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
			GameManager.Instance.ReSpawn();
			SoundManager.Instance.PlaySound("Respawn",0.5f);
			col.transform.parent.GetComponent<PlayerMovement>().isInWater = false;

        }
	}
}
