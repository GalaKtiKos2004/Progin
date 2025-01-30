using UnityEngine;
using System;

public class Health
{
    private float _currentHealth;
    private float _maxHealth;

    public event Action Died;
    public event Action<float, float> Changed;

    public Health(float health)
    {
        _currentHealth = health;
        _maxHealth = health;
    }

    public void TakeDamage(float damage)
    {
        _currentHealth = Mathf.Clamp(_currentHealth - damage, 0, _maxHealth);
        Changed?.Invoke(_currentHealth, _maxHealth);
        if (_currentHealth <= 0)
        {
            Died?.Invoke();
        }
    }
}
