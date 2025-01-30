using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private List<Item> _items = new List<Item>();
    [SerializeField] private List<ItemSlot> _itemSlots = new List<ItemSlot>();
    [SerializeField] private List<ItemSlot> _equipSlots = new List<ItemSlot>();
    [SerializeField] private InventoryUi _inventoryUi;

    public IReadOnlyList<Item> Items => _items;

    public void AddItem(Item item)
    {
        _items.Add(item);

        foreach (var slot in _itemSlots)
        {
            if (slot.CanSetItem)
            {
                slot.SetItem(item);
                return;
            }
        }

        Debug.Log("Нет свободных слотов в инвентаре!");
    }


    public void RemoveItem(Item item)
    {
        if (_items.Contains(item) == false)
        {
            return;
        }

        _items.Remove(item);

        foreach (var slot in _itemSlots)
        {
            if (slot.ItemInSlot == item)
            {
                slot.Clear();
                return;
            }
        }

        foreach (var slot in _equipSlots)
        {
            if (slot.ItemInSlot == item)
            {
                slot.Clear();
                return;
            }
        }

        Debug.Log($"Предмет удалён: {item.Name}");
    }

}
