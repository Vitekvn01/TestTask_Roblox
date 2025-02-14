using UnityEngine;

public class JumpingState : ICharacterState
{
    private readonly ICharacterContext _character;

    public JumpingState(ICharacterContext character)
    {
        _character = character;
    }

    public void UpdateState(IInputController inputController)
    {
        _character.Animator.SetBool("JumpFall", true);

        float horizontal = inputController.GetAxisHorizontal();
        float vertical = inputController.GetAxisVertical();

        Vector3 direction = (_character.CameraTransform.forward * vertical + _character.CameraTransform.right * horizontal).normalized;

        if (direction.magnitude >= 0.1f && _character.CharacterController.isGrounded != true)
        {
            _character.Move(direction * _character.Speed * Time.deltaTime);
            _character.CharacterController.transform.rotation = Quaternion.LookRotation(direction);
        }
        else
        {
            _character.Animator.SetBool("JumpFall", false);
            _character.ChangeState(StateType.Idle);
        }
    }
}
