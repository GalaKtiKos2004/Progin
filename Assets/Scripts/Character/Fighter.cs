using System.Collections;
using UnityEngine;

public abstract class Fighter : MonoBehaviour
{
    [SerializeField] private float _damage;
    [SerializeField] private float _cooldownTime;
    [SerializeField] private Trigger _trigger;

    private WaitForSeconds _cooldown;
    private Health _health;

    protected Health Health => _health;
    protected bool InTrigger { get; private set; } = false;
    protected Collider2D CollidedObject { get; private set; }
    protected bool IsCooldown { get; private set; } = false;

    private void OnEnable()
    {
        _cooldown = new WaitForSeconds(_cooldownTime);
        _trigger.TriggerEntered += OnTriggerEntered;
        _trigger.TriggerExited += OnTriggerExited;
    }

    private void OnDisable()
    {
        _trigger.TriggerEntered -= OnTriggerEntered;
        _trigger.TriggerExited -= OnTriggerExited;
        _health.Died -= OnDied;
    }

    public void Init(Health health)
    {
        _health = health;
        _health.Died += OnDied;
    }

    protected abstract void OnDied();

    protected void Attack(Fighter fighter)
    {
        if (IsCooldown == false)
        {
            fighter.TakeDamage(_damage);
            StartCoroutine(Cooldown());
        }
    }

    private void TakeDamage(float damage)
    {
        _health.TakeDamage(damage);
    }

    private void OnTriggerEntered(Collider2D other)
    {
        InTrigger = true;
        CollidedObject = other;
    }

    private void OnTriggerExited(Collider2D other) => InTrigger = false;


    private IEnumerator Cooldown()
    {
        IsCooldown = true;
        yield return _cooldown;
        IsCooldown = false;
    }
}
