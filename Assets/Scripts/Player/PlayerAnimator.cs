using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(PlayerMover))]
[RequireComponent(typeof(PlayerFighter))]
public class PlayerAnimator : MonoBehaviour
{
    private const string Speed = "Speed";
    private const string Attack = "Attack";
    private const string Death = "Death";
    private const string Win = "Win";

    [SerializeField] private LevelManager _levelManager;
    
    private Animator _animator;
    private PlayerMover _playerMover;
    private PlayerFighter _playerFighter;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _playerMover = GetComponent<PlayerMover>();
        _playerFighter = GetComponent<PlayerFighter>();
    }

    private void OnEnable()
    {
        _playerMover.Moved += OnMoved;
        _playerFighter.Attacked += OnAttack;
        _playerFighter.Died += OnDied;
        _levelManager.Won += OnWon;
        
    }

    private void OnDisable()
    {
        _playerMover.Moved -= OnMoved;
        _playerFighter.Attacked -= OnAttack;
        _playerFighter.Died -= OnDied;
        _levelManager.Won -= OnWon;
    }

    private void OnWon()
    {
        _animator.SetTrigger(Win);
    }

    private void OnMoved(float speed)
    {
        _animator.SetFloat(Speed, speed);
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
