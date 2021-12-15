using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
   private void Awake()
   {
      GameManager.Instance.player.gameObject.SetActive(true);
      GameManager.Instance.player.gameObject.transform.position = transform.position;
   }
}
