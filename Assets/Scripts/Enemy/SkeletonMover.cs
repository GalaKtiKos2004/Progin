using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class SkeletonMover : MonoBehaviour
{
    [Header("Цель и параметры движения")]
    [SerializeField] private Transform _target;         
    [SerializeField] private float _maxSpeed = 3f;       
    [SerializeField] private float _maxForce = 1f;       
    [SerializeField] private float _arrivalDistance = 0.5f; 

    [Header("Параметры зоны обнаружения")]
    [SerializeField] private float _detectionRange = 5f;  // Радиус видимости врага
    private bool _playerDetected = false; // Флаг обнаружения игрока

    [Header("Параметры обхода препятствий")]
    [SerializeField] private float _rayLength = 2f;       
    [SerializeField] private LayerMask _obstacleMask;     
    [SerializeField] private float _avoidMultiplier = 1f;  

    private Rigidbody2D _rb;

    public event Action<float> Moved;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Moved?.Invoke(_rb.velocity.magnitude);
    }

    private void FixedUpdate()
    {
        if (_target == null)
        {
            Debug.LogWarning("🚨 Враг не двигается: цель (_target) не назначена!", gameObject);
            return;
        }

        // Проверяем, находится ли игрок в зоне обнаружения
        float distanceToPlayer = Vector2.Distance(_target.position, _rb.position);
        _playerDetected = distanceToPlayer <= _detectionRange;

        if (!_playerDetected)
        {
            _rb.velocity = Vector2.zero; // Останавливаем движение
            return;
        }

        // 1. Вычисляем вектор, направленный к цели
        Vector2 desired = ((Vector2)_target.position - _rb.position);
        desired.Normalize();

        if (distanceToPlayer < _arrivalDistance)
        {
            desired *= (_maxSpeed * (distanceToPlayer / _arrivalDistance));
        }
        else
        {
            desired *= _maxSpeed;
        }

        // Основная управляющая сила (steering)
        Vector2 steer = desired - _rb.velocity;
        steer = Vector2.ClampMagnitude(steer, _maxForce);

        // 2. Проверяем препятствия перед врагом
        Vector2 rayDirection = _rb.velocity.normalized;
        if (rayDirection == Vector2.zero)
        {
            rayDirection = desired; // Если враг остановлен, используем направление к цели
        }

        RaycastHit2D hit = Physics2D.Raycast(_rb.position, rayDirection, _rayLength, _obstacleMask);
        if (hit.collider != null)
        {
            Debug.Log("⛔ Враг обнаружил препятствие: " + hit.collider.name);
            Vector2 avoidForce = hit.normal * _avoidMultiplier;
            steer += avoidForce;
        }

        // Применяем силу к Rigidbody2D
        _rb.AddForce(steer, ForceMode2D.Force);

        // Ограничиваем скорость
        if (_rb.velocity.magnitude > _maxSpeed)
        {
            _rb.velocity = _rb.velocity.normalized * _maxSpeed;
        }
    }

    // Визуализация зоны обнаружения в редакторе
    private void OnDrawGizmosSelected()
    {
        if (_rb == null)
            return;

        // Радиус обнаружения игрока
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, _detectionRange);

        if (_target == null)
            return;

        // Визуализация направления движения
        Gizmos.color = Color.red;
        Vector2 rayDirection = _rb.velocity.normalized;
        if (rayDirection == Vector2.zero)
        {
            rayDirection = (((Vector2)_target.position - _rb.position)).normalized;
        }
        Gizmos.DrawLine(transform.position, transform.position + (Vector3)(rayDirection * _rayLength));
    }
}
