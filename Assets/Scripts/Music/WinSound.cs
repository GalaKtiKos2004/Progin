using System;
using UnityEngine;

public class WinSound : MonoBehaviour
{
    [SerializeField] private GameSound _gameSound;
    [SerializeField] private AudioClip _clip;
    [SerializeField] private LevelManager _levelManager;

    private void OnEnable()
    {
        _levelManager.Won += OnWon;
    }

    private void OnDisable()
    {
        _levelManager.Won -= OnWon;
    }

    private void OnWon()
    {
        _gameSound.PlaySound(_clip);
    }
}
