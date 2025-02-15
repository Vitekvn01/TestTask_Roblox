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
        direction.y = 0f;

        if (direction.magnitude >= 0.1f && _playerController.CharacterView.isGrounded != true)
        {
            _playerController.CharacterView.Move(direction * _playerController.Speed * Time.deltaTime);
            _playerController.CharacterView.transform.rotation = Quaternion.LookRotation(direction);
        }
        
        if(_playerController.CharacterView.isGrounded)
        {
            _playerController.Animator.SetBool("JumpFall", false);
            Debug.Log("IsJumping JumpFall false");
            _playerController.ChangeState(StateType.Idle);
        }

        Debug.Log("IsJumping");
    }
}
