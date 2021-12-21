
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private float inputForward;
    private float inputSide;
    private float jump;
    private bool infectKeyPressed;
    private bool cureKeyPressed;

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

    public bool InfectKeyPressed
    {
        get => infectKeyPressed;
        set => infectKeyPressed = value;
    }

    public bool CureKeyPressed
    {
        get => cureKeyPressed;
        set => cureKeyPressed = value;
    }

    private void Update()
    {
        inputSide = Input.GetAxis("Horizontal");
        inputForward = Input.GetAxis("Vertical");
        jump = Input.GetAxisRaw("Jump");
        infectKeyPressed = Input.GetKey(KeyCode.Mouse0);
        cureKeyPressed = Input.GetKey(KeyCode.Mouse1);
    }
    

    public bool IsJumpKeyPressed()
    {
        if (jump <= 0)
            return false;
        return true;
    }
}
