using System;
using UnityEngine;

public interface IInputController
{
    public float GetAxisVertical();

    public float GetAxisHorizontal();

    public float GetAxisMouseY();

    public float GetAxisMouseX();

    public bool GetButtonJump();

}
