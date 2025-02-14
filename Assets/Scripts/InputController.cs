using UnityEngine;

public class InputController : MonoBehaviour, IInputController
{
    public float GetAxisHorizontal()
    {
        return Input.GetAxis("Horizontal");
    }

    public float GetAxisVertical()
    {
        return Input.GetAxis("Vertical");
    }

    public float GetAxisMouseX()
    {
        return Input.GetAxis("Mouse X");
    }

    public float GetAxisMouseY()
    {
        return Input.GetAxis("Mouse Y");
    }

    public bool GetButtonJump()
    {
        return Input.GetButtonDown("Jump");
    }
}
