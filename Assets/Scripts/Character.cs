using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float _speed = 5f; // �������� ��������
    [SerializeField] private float _jumpForce = 5f; // ���� ������
    [SerializeField] private float _gravity = -9.81f; // ����������
    [SerializeField] private float _groundCheckDistance = 0.2f; // ��������� �������� �����

    [Header("Collider Settings")]
    [SerializeField] private float _height = 2f; // ������ �������
    [SerializeField] private float _radius = 0.5f; // ������ �������

    private Rigidbody _rigidbody;
    private CapsuleCollider _collider;
    private Vector3 _velocity;
    private bool _isGrounded;

    public bool IsGrounded => _isGrounded; // �������� ��� ��������, ��������� �� �������� �� �����
    public Vector3 Velocity => _velocity; // ������� ��������

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _collider = GetComponent<CapsuleCollider>();

        // ��������� Rigidbody
        _rigidbody.interpolation = RigidbodyInterpolation.Interpolate;
        _rigidbody.freezeRotation = true; // ��������� ��������

        // ��������� CapsuleCollider
        _collider.height = _height;
        _collider.radius = _radius;
        _collider.center = new Vector3(0, _height / 2, 0);
    }

    private void FixedUpdate()
    {
        CheckGrounded();
        ApplyGravity();
    }

    /// <summary>
    /// ���������� ��������� � �������� �����������.
    /// </summary>
    /// <param name="direction">����������� �������� (��������������� ������).</param>
    public void Move(Vector3 direction)
    {
        if (_isGrounded)
        {
            _velocity.x = direction.x * _speed;
            _velocity.z = direction.z * _speed;
        }

        // ��������� �������� ����� Rigidbody
        _rigidbody.velocity = new Vector3(_velocity.x, _rigidbody.velocity.y, _velocity.z);
    }

    /// <summary>
    /// ���������� ��������� ��������.
    /// </summary>
    public void Jump()
    {
        if (_isGrounded)
        {
            _velocity.y = _jumpForce;
            _rigidbody.velocity = new Vector3(_rigidbody.velocity.x, _velocity.y, _rigidbody.velocity.z);
        }
    }

    /// <summary>
    /// ���������, ��������� �� �������� �� �����.
    /// </summary>
    private void CheckGrounded()
    {
        Vector3 raycastOrigin = transform.position + _collider.center;
        float raycastDistance = _collider.height / 2 + _groundCheckDistance;

        _isGrounded = Physics.Raycast(raycastOrigin, Vector3.down, raycastDistance);
    }

    /// <summary>
    /// ��������� ���������� � ���������.
    /// </summary>
    private void ApplyGravity()
    {
        if (!_isGrounded)
        {
            _velocity.y += _gravity * Time.fixedDeltaTime;
        }
        else if (_velocity.y < 0)
        {
            _velocity.y = -2f; // ��������� ����, ����� �������� "��������" � �����
        }

        // ��������� ������������ �������� ����� Rigidbody
        _rigidbody.velocity = new Vector3(_rigidbody.velocity.x, _velocity.y, _rigidbody.velocity.z);
    }
}
