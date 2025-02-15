using UnityEngine;

public interface ICharacterContext
{
    public CharacterController CharacterView { get; }
    public Animator Animator { get; }
    public float Speed { get; }
    public float Gravity { get; }
    public Transform CameraTransform { get; }
    public float JumpForce { get; }

    public void ChangeState(StateType newState);

    public void Jump();

    public void Move(Vector3 direction);

}
