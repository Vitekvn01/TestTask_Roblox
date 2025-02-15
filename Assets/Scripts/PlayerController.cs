using Cinemachine;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlayerController : MonoBehaviour, ICharacterContext
{
    [SerializeField] private Transform _cameraTransform;
    [SerializeField] private CinemachineFreeLook _cinemachineCam;
    [SerializeField] private CharacterController _characterView;
    [SerializeField] private Animator _animator;
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _jumpForce = 2f;
    [SerializeField] private float _gravity = -10f;

    private Vector3 velocity;

    private IInputController _inputController;
    private ICharacterState _currentState;
    private Dictionary<StateType, ICharacterState> states;

    public CharacterController CharacterView => _characterView;
    public Animator Animator => _animator;
    public float Speed => _speed;
    public float Gravity => _gravity;
    public Transform CameraTransform => _cameraTransform;
    public float JumpForce => _jumpForce;

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

        ApplyGravity();
    }

    public void ChangeState(StateType newState)
    {
        _currentState = states[newState];
    }

    private void ApplyGravity()
    {
        if (!_characterView.isGrounded)
        {
            velocity.y += _gravity * Time.deltaTime;
        }
        else if (velocity.y < 0)
        {
            velocity.y = -2f;
        }

        _characterView.Move(velocity * Time.deltaTime);
    }

    public void Move(Vector3 direction)
    {
        _characterView.Move(direction * Time.deltaTime);
    }

    public void Jump()
    {
        if (_characterView.isGrounded)
        {
            velocity.y = _jumpForce;
            ChangeState(StateType.Jumping);
        }
    }
}

public enum StateType 
{
    Idle,
    Running,
    Jumping
}
