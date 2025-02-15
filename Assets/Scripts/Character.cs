using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float _speed = 5f; // Скорость движения
    [SerializeField] private float _jumpForce = 5f; // Сила прыжка
    [SerializeField] private float _gravity = -9.81f; // Гравитация
    [SerializeField] private float _groundCheckDistance = 0.2f; // Дистанция проверки земли

    [Header("Collider Settings")]
    [SerializeField] private float _height = 2f; // Высота капсулы
    [SerializeField] private float _radius = 0.5f; // Радиус капсулы

    private Rigidbody _rigidbody;
    private CapsuleCollider _collider;
    private Vector3 _velocity;
    private bool _isGrounded;

    public bool IsGrounded => _isGrounded; // Свойство для проверки, находится ли персонаж на земле
    public Vector3 Velocity => _velocity; // Текущая скорость

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _collider = GetComponent<CapsuleCollider>();

        // Настройка Rigidbody
        _rigidbody.interpolation = RigidbodyInterpolation.Interpolate;
        _rigidbody.freezeRotation = true; // Запрещаем вращение

        // Настройка CapsuleCollider
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
    /// Перемещает персонажа в заданном направлении.
    /// </summary>
    /// <param name="direction">Направление движения (нормализованный вектор).</param>
    public void Move(Vector3 direction)
    {
        if (_isGrounded)
        {
            _velocity.x = direction.x * _speed;
            _velocity.z = direction.z * _speed;
        }

        // Применяем движение через Rigidbody
        _rigidbody.velocity = new Vector3(_velocity.x, _rigidbody.velocity.y, _velocity.z);
    }

    /// <summary>
    /// Заставляет персонажа прыгнуть.
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
    /// Проверяет, находится ли персонаж на земле.
    /// </summary>
    private void CheckGrounded()
    {
        Vector3 raycastOrigin = transform.position + _collider.center;
        float raycastDistance = _collider.height / 2 + _groundCheckDistance;

        _isGrounded = Physics.Raycast(raycastOrigin, Vector3.down, raycastDistance);
    }

    /// <summary>
    /// Применяет гравитацию к персонажу.
    /// </summary>
    private void ApplyGravity()
    {
        if (!_isGrounded)
        {
            _velocity.y += _gravity * Time.fixedDeltaTime;
        }
        else if (_velocity.y < 0)
        {
            _velocity.y = -2f; // Небольшая сила, чтобы персонаж "прилипал" к земле
        }

        // Применяем вертикальную скорость через Rigidbody
        _rigidbody.velocity = new Vector3(_rigidbody.velocity.x, _velocity.y, _rigidbody.velocity.z);
    }
}
