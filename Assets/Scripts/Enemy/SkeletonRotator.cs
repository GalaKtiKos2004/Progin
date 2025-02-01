using System;
using UnityEngine;

/// <summary>
/// Класс, отвечающий за поворот скелета в сторону цели.
/// Автоматически поворачивает объект в сторону _target.
/// </summary>
[RequireComponent(typeof(EnemyFighter))]
public class SkeletonRotator : MonoBehaviour
{
    /// <summary>
    /// Цель, на которую должен смотреть скелет.
    /// </summary>
    [SerializeField] private Transform _target;

    private readonly Quaternion _leftRotation = Quaternion.Euler(0, 180, 0);
    private readonly Quaternion _rightRotation = Quaternion.Euler(0, 0, 0);
    
    private EnemyFighter _fighter;
    private bool _canRotate = true;

    private void Awake()
    {
        _fighter = GetComponent<EnemyFighter>();
    }

    private void OnEnable()
    {
        _fighter.Died += OnDied;
    }

    private void OnDisable()
    {
        _fighter.Died -= OnDied;
    }

    /// <summary>
    /// Проверяет позицию цели и поворачивает скелет в её сторону.
    /// </summary>
    private void Update()
    {
        if (_canRotate == false)
        {
            return;
        }
        
        if (_target == null)
        {
            return;
        }

        if (_target.position.x > transform.position.x)
        {
            transform.rotation = _rightRotation;
        }
        else
        {
            transform.rotation = _leftRotation;
        }
    }

    /// <summary>
    /// Останавливает поворот скелета при его смерти.
    /// </summary>
    private void OnDied()
    {
        _canRotate = false;
    }
}
