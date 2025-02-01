using System.Collections;
using UnityEngine;

public abstract class Fighter : MonoBehaviour
{
    [SerializeField] private float _damage;
    [SerializeField] private float _cooldownTime;
    [SerializeField] private float _baseDefense;
    [SerializeField] private Trigger _trigger;

    private WaitForSeconds _cooldown;
    private Health _health;

    protected Health Health => _health;
    protected bool InTrigger { get; private set; } = false;
    protected Collider2D CollidedObject { get; private set; }
    protected bool IsCooldown { get; private set; } = false;
    
    public bool IsDead { get; protected set; } = false;

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

    protected virtual void OnDied()
    {
        IsDead = true;
    }

    protected void Attack(Fighter fighter)
    {
        if (IsCooldown == false && fighter.IsDead == false)
        {
            float totalDamage = GetTotalDamage();
            fighter.TakeDamage(totalDamage);
            StartCoroutine(Cooldown());
        }
    }

    private void TakeDamage(float damage)
    {
        float totalDefense = GetTotalDefense();
        float finalDamage = Mathf.Max(damage - totalDefense, 0);
        _health.TakeDamage(finalDamage);
    }

    protected virtual float GetTotalDamage()
    {
        return _damage;
    }

    protected virtual float GetTotalDefense()
    {
        return _baseDefense;
    }

    private void OnTriggerEntered(Collider2D other)
    {
        if (other.TryGetComponent(out Fighter _) == false)
        {
            return;
        }
        
        InTrigger = true;
        CollidedObject = other;
    }

    private void OnTriggerExited(Collider2D other)
    {
        if (other.TryGetComponent(out Fighter _) == false)
        {
            return;
        }
        
        InTrigger = false;
    }

    private IEnumerator Cooldown()
    {
        IsCooldown = true;
        yield return _cooldown;
        IsCooldown = false;
    }
}
