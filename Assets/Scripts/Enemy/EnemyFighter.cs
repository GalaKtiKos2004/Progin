using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class EnemyFighter : Fighter
{
    [SerializeField] private Animator _animator;
    [SerializeField] private float _dyingTime;
    
    private SpriteRenderer _spriteRenderer;
    private bool _animationEnding = true;
    private bool _isDead = false;
    
    public event Action Attacked;
    public event Action Died;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (InTrigger == false || _animationEnding == false || _isDead || IsCooldown)
        {
            return;
        }

        if (CollidedObject.TryGetComponent(out PlayerFighter playerFighter))
        {
            Attack(playerFighter);
            Attacked?.Invoke();
            _animationEnding = false;
        }
    }
    
    protected override void OnDied()
    {
        Died?.Invoke();
        _isDead = true;
        StartCoroutine(Dying());
    }

    private void AnimationEnded()
    {
        _animationEnding = true;
    }

    private IEnumerator Dying()
    {
        float elapsedTime = 0f;
        Color color = _spriteRenderer.color;

        while (elapsedTime < _dyingTime)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / _dyingTime);
            _spriteRenderer.color = new Color(color.r, color.g, color.b, alpha);
            yield return null;
        }

        Destroy(gameObject);
    }
}
