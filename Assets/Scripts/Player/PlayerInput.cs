using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private float inputForward;
    private float inputSide;
    private float jump;

    public float InputForward
    {
        get => inputForward;
        set => inputForward = value;
    }

    public float InputSide
    {
        get => inputSide;
        set => inputSide = value;
    }

    public float Jump
    {
        get => jump;
        set => jump = value;
    }

    private void Update()
    {
        inputSide = Input.GetAxis("Horizontal");
        inputForward = Input.GetAxis("Vertical");
        jump = Input.GetAxis("Jump");
        Debug.Log(jump);
    }
}
