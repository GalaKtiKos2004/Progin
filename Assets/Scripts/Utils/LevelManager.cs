using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private List<EnemyFighter> _enemys;
    [SerializeField] private Inventory _inventory;
    [SerializeField] private SceneController _sceneController;
    [SerializeField] private float _delay = 3f;
    [SerializeField] private string _newSceneName;

    private WaitForSeconds _wait;
    private int _diedCount = 0;

    public event Action Won;

    private void OnEnable()
    {
        _wait = new WaitForSeconds(_delay);
        
        foreach (var enemy in _enemys)
        {
            enemy.Died += OnDied;
        }
    }

    private void OnDisable()
    {
        foreach (var enemy in _enemys)
        {
            enemy.Died -= OnDied;
        }
    }

    private void OnDied()
    {
        _diedCount++;

        if (_diedCount == _enemys.Count)
        {
            Won?.Invoke();
            StartCoroutine(NewLevelDelay());
        }
    }

    private IEnumerator NewLevelDelay()
    {
        yield return _wait;
        
        _sceneController.LoadScene(_newSceneName, _inventory);
    }
}
