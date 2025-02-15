using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _jumpForce = 5f;
    [SerializeField] private float _gravity = -9.81f;
    [SerializeField] private float _groundCheckDistance = 0.2f;


    private Rigidbody _rigidbody;
    private CapsuleCollider _collider;
    private Vector3 _velocity;
    private bool _isGrounded;

    public bool IsGrounded => _isGrounded;
    public Vector3 Velocity => _velocity;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _collider = GetComponent<CapsuleCollider>();

        _rigidbody.interpolation = RigidbodyInterpolation.Interpolate;
        _rigidbody.freezeRotation = true;
    }

    private void Update()
    {
        CheckGrounded();
    }

    private void FixedUpdate()
    {
        ApplyGravity();
        ApplyMovement();
    }

    public void Move(Vector3 direction)
    {
        if (direction.magnitude != 0f)
        {
            if (_isGrounded)
            {
                _velocity.x = direction.x * _speed;
                _velocity.z = direction.z * _speed;
            }
            else
            {
                float airControlSpeed = _speed * 0.5f;
                _velocity.x = direction.x * airControlSpeed;
                _velocity.z = direction.z * airControlSpeed;
            }
        }
        else
        {

            _velocity.x = 0f;
            _velocity.z = 0f;
        }


    }

    public void Rotate(Vector3 direction)
    {
        if (direction.magnitude != 0f)
        {
            transform.rotation = Quaternion.LookRotation(direction);
        }

    }

    public void Jump()
    {
        if (_isGrounded)
        {
            _velocity.y = _jumpForce;
            _rigidbody.velocity = new Vector3(_rigidbody.velocity.x, _velocity.y, _rigidbody.velocity.z);
        }
    }

    private void CheckGrounded()
    {
        Vector3 raycastOrigin = transform.position + _collider.center;
        float raycastDistance = _collider.height / 2 + _groundCheckDistance;

        _isGrounded = Physics.Raycast(raycastOrigin, Vector3.down, raycastDistance);

        Debug.DrawRay(raycastOrigin, Vector3.down * raycastDistance, _isGrounded ? Color.green : Color.red);
    }

    private void ApplyGravity()
    {
        if (!_isGrounded)
        {
            _velocity.y += _gravity * Time.fixedDeltaTime;
        }
        else if (_velocity.y < 0)
        {
            _velocity.y = -2f;
        }

        _rigidbody.velocity = new Vector3(_rigidbody.velocity.x, _velocity.y, _rigidbody.velocity.z);
    }

    private void ApplyMovement()
    {
        _rigidbody.velocity = new Vector3(_velocity.x, _rigidbody.velocity.y, _velocity.z);
    }
}
