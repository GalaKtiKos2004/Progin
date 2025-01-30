using System;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Chest))]
public class ChestAnimator : MonoBehaviour
{
    private const string Open = "Open";
    
    private Chest _chest;
    private Animator _animator;

    private void Awake()
    {
        _chest = GetComponent<Chest>();
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _chest.Opening += OnOpening;
    }

    private void OnDisable()
    {
        _chest.Opening -= OnOpening;
    }

    private void OnOpening()
    {
        _animator.SetTrigger(Open);
    }
}
