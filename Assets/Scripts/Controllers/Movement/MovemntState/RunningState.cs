using UnityEngine;

public class RunningState : ICharacterState
{
    private readonly ICharacterContext _playerController;

    public RunningState(ICharacterContext character)
    {
        _playerController = character;
    }

    public void UpdateState(IInputController inputController)
    {
        float horizontal = inputController.GetAxisHorizontal();
        float vertical = inputController.GetAxisVertical();

        Vector3 direction = (_playerController.CameraTransform.forward * vertical + _playerController.CameraTransform.right * horizontal).normalized;
        direction.y = 0f;

        _playerController.Animator.SetBool("isRunning", true);
        _playerController.CharacterView.Move(direction);
        _playerController.CharacterView.Rotate(direction);

        if (direction.magnitude == 0f)
        {
            _playerController.ChangeState(StateType.Idle);
        }



        if (inputController.GetButtonJump() && _playerController.CharacterView.IsGrounded)
        {
            _playerController.CharacterView.Jump();
            _playerController.Animator.SetTrigger("JumpUp");
            _playerController.ChangeState(StateType.Jumping);
        }

        if (_playerController.CharacterView.IsGrounded == false)
        {
            _playerController.ChangeState(StateType.Jumping);
        }

        Debug.Log("IsRunning");
    }
}
