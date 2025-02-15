using UnityEngine;

public interface ICharacterContext
{
    public Character CharacterView { get; }
    public Animator Animator { get; }
    public Transform CameraTransform { get; }

    public void ChangeState(StateType newState);

}
