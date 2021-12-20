using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
   public Vector3 LoadSpawnPoint()
   {
       return transform.position;
   }
   
   private void Start()
   {
   }
}
