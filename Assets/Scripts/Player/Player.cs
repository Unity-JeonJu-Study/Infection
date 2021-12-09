using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Item
{
    MasterKey,
}

public class Player : MonoBehaviour
{
    public List<Item> inventory;
}
