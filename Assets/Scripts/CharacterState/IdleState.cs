public class IdleState : ICharacterState
{
    private readonly ICharacterContext _character;

    public IdleState(ICharacterContext character)
    {
        _character = character;
    }

    public void UpdateState(IInputController inputController)
    {
        float horizontal = inputController.GetAxisHorizontal();
        float vertical = inputController.GetAxisVertical();

        if (horizontal != 0 || vertical != 0)
        {
            _character.ChangeState(StateType.Running);
        }
        _character.Animator.SetBool("isRunning", false);
    }
}
