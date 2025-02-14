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

        if (direction.magnitude >= 0.1f)
        {
            _playerController.Move(direction * _playerController.Speed * Time.deltaTime);
            _playerController.CharacterController.transform.rotation = Quaternion.LookRotation(direction);
        }
        else
        {
            _playerController.ChangeState(StateType.Idle);
        }

        _playerController.Animator.SetBool("isRunning", true);

        if (inputController.GetButtonJump() && _playerController.CharacterController.isGrounded)
        {
            _playerController.Animator.SetTrigger("JumpUp");
            _playerController.Jump();
            _playerController.ChangeState(StateType.Jumping);
        }
    }
}
