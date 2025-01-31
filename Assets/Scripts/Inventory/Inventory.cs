using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private const int MaxItems = 15;

    [SerializeField] private List<Item> _items = new List<Item>();
    [SerializeField] private List<ItemSlot> _itemSlots = new List<ItemSlot>();
    [SerializeField] private List<ItemSlot> _equipSlots = new List<ItemSlot>();
    [SerializeField] private InventoryUi _inventoryUi;

    public IReadOnlyList<Item> Items => _items;
    
    public bool AddItem(Item item)
    {
        if (_items.Count >= MaxItems)
        {
            Debug.Log("Инвентарь заполнен!");
            return false;
        }

        _items.Add(item);

        foreach (var slot in _itemSlots)
        {
            if (slot.CanSetItem)
            {
                slot.SetItem(item);
                return true;
            }
        }

        Debug.Log("Нет свободных слотов в инвентаре!");
        return false;
    }

    public void RemoveItemFromSlot(ItemSlot selectedSlot)
    {
        if (selectedSlot == null || selectedSlot.ItemInSlot == null)
        {
            Debug.Log("Пустой слот, нечего удалять!");
            return;
        }

        Item itemToRemove = selectedSlot.ItemInSlot;

        if (_items.Contains(itemToRemove))
        {
            _items.Remove(itemToRemove);
            selectedSlot.Clear();
            Debug.Log($"Удален предмет: {itemToRemove.Name}");
        }
    }

    public void LoadFromOtherInventory(IReadOnlyList<Item> items)
    {
        _items.Clear();

        foreach (var item in items)
        {
            AddItem(item);
        }
    }
}
