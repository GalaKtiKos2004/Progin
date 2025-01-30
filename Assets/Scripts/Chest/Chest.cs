using System;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    private const KeyCode OpenKey = KeyCode.Return;
    
    [SerializeField] private Trigger _trigger;
    [SerializeField] private Inventory _inventory;
    [SerializeField] private List<Item> _items;
    
    private bool _canOpen = false;
    private bool _isOpen = false;
    
    public event Action Opening;

    private void OnEnable()
    {
        _trigger.TriggerEntered += OnTriggerEntered;
        _trigger.TriggerExited += OnTriggerExited;
    }

    private void OnDisable()
    {
        _trigger.TriggerEntered -= OnTriggerEntered;
        _trigger.TriggerExited -= OnTriggerExited;
    }

    private void Update()
    {
        if (Input.GetKeyDown(OpenKey) && _canOpen && _isOpen == false)
        {
            Opening?.Invoke();
            Open();
            _isOpen = true;
        }
    }

    private void OnTriggerEntered(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerMover _))
        {
            _canOpen = true;
        }
    }
    
    private void OnTriggerExited(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerMover _))
        {
            _canOpen = false;
        }
    }

    private void Open()
    {
        Item item = GetRandomItem();
        
        if (item != null && _inventory != null)
        {
            _inventory.AddItem(item);
            Debug.Log($"Игрок получил {item.Name} из сундука!");
        }
    }

    private Item GetRandomItem()
    {
        float totalWeight = 0f;

        foreach (var loot in _items)
        {
            totalWeight += loot.DropChance;
        }
        
        float randomValue = UnityEngine.Random.Range(0f, totalWeight);
        float currentSum = 0f;
        
        foreach (var loot in _items)
        {
            currentSum += loot.DropChance;
            if (randomValue <= currentSum)
            {
                return loot;
            }
        }
        
        return null;
    }
}
