using UnityEngine;
public class IdleState : ICharacterState
{
    private readonly ICharacterContext _playerController;

    public IdleState(ICharacterContext character)
    {
        _playerController = character;
    }

    public void UpdateState(IInputController inputController)
    {
        float horizontal = inputController.GetAxisHorizontal();
        float vertical = inputController.GetAxisVertical();

        if (horizontal != 0 || vertical != 0)
        {
            _playerController.Move(new Vector3(0,0,0));
            _playerController.ChangeState(StateType.Running);
        }
        _playerController.Animator.SetBool("isRunning", false);
    }
}
