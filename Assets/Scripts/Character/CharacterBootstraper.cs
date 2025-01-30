using UnityEngine;

public class CharacterBootstraper : MonoBehaviour
{
    [SerializeField] private float _health;
    [SerializeField] private Fighter _fighter;
    [SerializeField] private HealthBar _healthBar;

    private void Awake()
    {
        Health health = new(_health);
        _fighter.Init(health);
        _healthBar.Init(health);
    }
}
