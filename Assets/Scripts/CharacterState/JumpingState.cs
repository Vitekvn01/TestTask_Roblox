using UnityEngine;

public class JumpingState : ICharacterState
{
    private readonly ICharacterContext _playerController;

    public JumpingState(ICharacterContext character)
    {
        _playerController = character;
    }

    public void UpdateState(IInputController inputController)
    {
        _playerController.Animator.SetBool("JumpFall", true);

        float horizontal = inputController.GetAxisHorizontal();
        float vertical = inputController.GetAxisVertical();

        Vector3 direction = (_playerController.CameraTransform.forward * vertical + _playerController.CameraTransform.right * horizontal).normalized;

        if (direction.magnitude >= 0.1f && _playerController.CharacterController.isGrounded != true)
        {
            _playerController.Move(direction * _playerController.Speed * Time.deltaTime);
            _playerController.CharacterController.transform.rotation = Quaternion.LookRotation(direction);
        }
        else
        {
            _playerController.Animator.SetBool("JumpFall", false);
            _playerController.ChangeState(StateType.Idle);
        }
    }
}
