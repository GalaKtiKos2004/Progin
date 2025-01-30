using UnityEngine;
using UnityEngine.UI;

public class HealthBar : Bar
{
    [SerializeField] private Image _bar;
    [SerializeField] private float _smooothDecreaseDuration = 0.25f;

    private Health _health;

    private void OnDisable()
    {
        _health.Changed -= UpdateBar;
        _health.Died -= DeleteBar;
    }

    public void Init(Health health)
    {
        _health = health;

        _health.Changed += UpdateBar;
        _health.Died += DeleteBar;
    }

    private void UpdateBar(float value, float maxValue)
    {
        StartCoroutine(DecreaseBarSmoothly(value / maxValue, _smooothDecreaseDuration));
    }

    private void DeleteBar()
    {
        Destroy(gameObject);
        Destroy(_bar.gameObject);
    }
}
