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
        float horizontal = inputController.GetAxisHorizontal();
        float vertical = inputController.GetAxisVertical();

        Vector3 direction = (_playerController.CameraTransform.forward * vertical + _playerController.CameraTransform.right * horizontal).normalized;
        direction.y = 0f;

        _playerController.CharacterView.Move(direction);
        _playerController.CharacterView.Rotate(direction);

        if (_playerController.CharacterView.IsGrounded)
        {
            _playerController.Animator.SetBool("Fall", false);
            _playerController.ChangeState(StateType.Idle);
        }
        else
        {
            _playerController.Animator.SetBool("Fall", true);
        }

        Debug.Log("IsJumping");
    }
}
