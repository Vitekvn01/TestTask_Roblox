using Cinemachine;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class MovementControllerFSM : MonoBehaviour, ICharacterContext
{
    [SerializeField] private Transform _cameraTransform;
    [SerializeField] private CinemachineFreeLook _cinemachineCam;
    [SerializeField] private Character _characterView;
    [SerializeField] private Animator _animator;

    private IInputController _inputController;
    private ICharacterState _currentState;
    private Dictionary<StateType, ICharacterState> states;

    public Character CharacterView => _characterView;
    public Animator Animator => _animator;
    public Transform CameraTransform => _cameraTransform;

    [Inject]
    private void Construct(IInputController inputController)
    {
        _inputController = inputController;

        states = new Dictionary<StateType, ICharacterState>
        {
            { StateType.Idle, new IdleState(this) },
            { StateType.Running, new RunningState(this) },
            { StateType.Jumping, new JumpingState(this) }
        };

        _currentState = states[StateType.Idle];
    }

    private void Update()
    {
        _currentState.UpdateState(_inputController);

    }

    public void ChangeState(StateType newState)
    {
        _currentState = states[newState];
    }
}

public enum StateType 
{
    Idle,
    Running,
    Jumping
}
