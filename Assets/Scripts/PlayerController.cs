using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlayerController : MonoBehaviour, ICharacterContext
{
    [SerializeField] private Transform _cameraTransform;
    [SerializeField] private CinemachineFreeLook _cinemachineCam;
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private Animator _animator;
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _jumpForce = 2f;
    [SerializeField] private float _gravity = -10f;

    private Vector3 velocity;

    private IInputController _inputController;
    private ICharacterState _currentState;
    private Dictionary<StateType, ICharacterState> states;

    public CharacterController CharacterController => _characterController;
    public Animator Animator => _animator;
    public float Speed => _speed;
    public float Gravity => _gravity;
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
        ApplyGravity();
        ApplyMovement();
    }

    public void ChangeState(StateType newState)
    {
        _currentState = states[newState];
    }

    private void ApplyGravity()
    {
        if (!_characterController.isGrounded)
        {
            velocity.y += _gravity * Time.deltaTime;
        }
        else if (velocity.y < 0)
        {
            velocity.y = -2f;
        }
    }

    private void ApplyMovement()
    {
        _characterController.Move(velocity * Time.deltaTime);
    }

    public void Move(Vector3 direction)
    {
        velocity.x = direction.x * _speed;
        velocity.z = direction.z * _speed;
    }

    public void Jump()
    {
        if (_characterController.isGrounded)
        {
            velocity.y = _jumpForce;
            ChangeState(StateType.Jumping);
        }
    }
}

public enum StateType { Idle, Running, Jumping }
