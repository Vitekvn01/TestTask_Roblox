using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICharacterContext
{
    public CharacterController CharacterController { get; }
    public Animator Animator { get; }
    public float Speed { get; }
    public float Gravity { get; }
    public Transform CameraTransform { get; }

    public void ChangeState(StateType newState);
    public void Move(Vector3 direction);
    public void Jump();


}
