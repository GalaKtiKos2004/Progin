using System;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(EnemyFighter))]
[RequireComponent(typeof(Animator))]
public class EnemyAnimator : MonoBehaviour
{
    private const string Attack = "Attack";
    private const string Death = "Death";
    
    private Animator _animator;
    private EnemyFighter _enemyFighter;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _enemyFighter = GetComponent<EnemyFighter>();
    }

    private void OnEnable()
    {
        _enemyFighter.Attacked += OnAttack;
        _enemyFighter.Died += OnDied;
    }

    private void OnDisable()
    {
        _enemyFighter.Attacked -= OnAttack;
        _enemyFighter.Died -= OnDied;
    }

    private void OnAttack()
    {
        _animator.SetTrigger(Attack);
    }

    private void OnDied()
    {
        _animator.SetTrigger(Death);
    }
}
