using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private PlayerFighter _playerFighter;

    private Rigidbody2D _rigidbody;
    private Vector2 _movementInput;
    private bool _isDied = false;

    private readonly Vector2 _defaultScale = new Vector2(1, 1);
    private readonly Vector2 _flippedScale = new Vector2(-1, 1);
    
    public event Action<float> Moved;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _playerFighter = GetComponent<PlayerFighter>();
        _rigidbody.gravityScale = 0; // Отключаем гравитацию для 2D-игрока
        _rigidbody.constraints = RigidbodyConstraints2D.FreezeRotation; // Замораживаем вращение
    }

    private void OnEnable()
    {
        _playerFighter.Died += OnDied;
    }

    private void OnDisable()
    {
        _playerFighter.Died -= OnDied;
    }

    private void OnDied()
    {
        _isDied = true;
        _rigidbody.velocity = Vector2.zero;
        _moveSpeed = 0f;    
    }

    private void Update()
    {
        if (_isDied)
        {
            return;
        }
        
        // Получаем ввод
        _movementInput.x = Input.GetAxisRaw("Horizontal");
        _movementInput.y = Input.GetAxisRaw("Vertical");
        
        Moved?.Invoke(Mathf.Max(Mathf.Abs(_movementInput.x), Mathf.Abs(_movementInput.y)));

        _movementInput = _movementInput.normalized;

        // Поворот
        if (_movementInput.x > 0)
        {
            transform.localScale = _defaultScale;
        }
        else if (_movementInput.x < 0)
        {
            transform.localScale = _flippedScale;
        }
    }

    private void FixedUpdate()
    {
        // Перемещение через физику
        _rigidbody.velocity = _movementInput * _moveSpeed;
    }
}
