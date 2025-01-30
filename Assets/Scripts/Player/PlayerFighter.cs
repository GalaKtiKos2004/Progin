using System;
using UnityEngine;

public class PlayerFighter : Fighter
{
    public event Action Attacked;

    private void Update()
    {
        if (IsCooldown)
        {
            return;
        }
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Attacked?.Invoke();

            if (InTrigger && CollidedObject.TryGetComponent(out EnemyFighter enemyFighter))
            {
                Attack(enemyFighter);
            }
        }
    }

    protected override void OnDied()
    {
        
    }
}
