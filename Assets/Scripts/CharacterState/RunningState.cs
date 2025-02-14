using UnityEngine;
using UnityEngine.TextCore.Text;

public class RunningState : ICharacterState
{
    private readonly ICharacterContext _character;

    public RunningState(ICharacterContext character)
    {
        _character = character;
    }

    public void UpdateState(IInputController inputController)
    {
        float horizontal = inputController.GetAxisHorizontal();
        float vertical = inputController.GetAxisVertical();

        Vector3 direction = (_character.CameraTransform.forward * vertical + _character.CameraTransform.right * horizontal).normalized;
        direction.y = 0f;

        if (direction.magnitude >= 0.1f)
        {
            _character.Move(direction * _character.Speed * Time.deltaTime);
            _character.CharacterController.transform.rotation = Quaternion.LookRotation(direction);
        }
        else
        {
            _character.ChangeState(StateType.Idle);
        }

        _character.Animator.SetBool("isRunning", true);

        if (inputController.GetButtonJump() && _character.CharacterController.isGrounded)
        {
            _character.Animator.SetTrigger("JumpUp");
            _character.Jump();
            _character.ChangeState(StateType.Jumping);
        }
    }
}
