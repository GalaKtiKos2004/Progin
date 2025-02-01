using UnityEngine;

/// <summary>
/// Управляет анимациями врага, реагируя на события атаки и смерти.
/// </summary>
[RequireComponent(typeof(EnemyFighter))]
[RequireComponent(typeof(Animator))]
public class EnemyAnimator : MonoBehaviour
{
    private const string Attack = "Attack";
    private const string Death = "Death";
    
    private Animator _animator;
    private EnemyFighter _enemyFighter;

    /// <summary>
    /// Инициализирует компоненты Animator и EnemyFighter.
    /// </summary>
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _enemyFighter = GetComponent<EnemyFighter>();
    }

    /// <summary>
    /// Подписывается на события атаки и смерти врага.
    /// </summary>
    private void OnEnable()
    {
        _enemyFighter.Attacked += OnAttack;
        _enemyFighter.Died += OnDied;
    }

    /// <summary>
    /// Отписывается от событий атаки и смерти врага.
    /// </summary>
    private void OnDisable()
    {
        _enemyFighter.Attacked -= OnAttack;
        _enemyFighter.Died -= OnDied;
    }

    /// <summary>
    /// Вызывается при атаке врага. Запускает анимацию атаки.
    /// </summary>
    private void OnAttack()
    {
        _animator.SetTrigger(Attack);
    }

    /// <summary>
    /// Вызывается при смерти врага. Запускает анимацию смерти.
    /// </summary>
    private void OnDied()
    {
        _animator.SetTrigger(Death);
    }
}
