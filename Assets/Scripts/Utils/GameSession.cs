using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour
{
    public static GameSession Instance { get; private set; }
    private IReadOnlyList<Item> _items;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SaveInventory(Inventory inventory)
    {
        _items = inventory.Items;
    }

    public void LoadInventory(Inventory newInventory)
    {
        Debug.Log("Loading inventory");
        if (_items != null)
        {
            Debug.Log("new Inventory loaded");
            newInventory.LoadFromOtherInventory(_items);
        }
    }
}
