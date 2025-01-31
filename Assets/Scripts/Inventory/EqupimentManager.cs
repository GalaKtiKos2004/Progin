using UnityEngine;

public class EqupimentManager : MonoBehaviour
{
    [SerializeField] private Inventory _inventory;

    [SerializeField] private ItemSlot _weaponSlot;
    [SerializeField] private ItemSlot _armorSlot;
    [SerializeField] private ItemSlot _shieldSlot;
    [SerializeField] private ItemSlot _helmetSlot;
    
    public Item Weapon => _weaponSlot.ItemInSlot;
    public Item Armor => _armorSlot.ItemInSlot;
    public Item Shield => _shieldSlot.ItemInSlot;
    public Item Helmet => _helmetSlot.ItemInSlot;

    public void EquipItem(Item item)
    {
        if (item.IsSword)
        {
            SwapEquipment(_weaponSlot, item);
        }
        else if (item.IsArmor)
        {
            SwapEquipment(_armorSlot, item);
        }
        else if (item.IsShield)
        {
            SwapEquipment(_shieldSlot, item);
        }
        else if (item.IsHelmet)
        {
            SwapEquipment(_helmetSlot, item);
        }
    }

    public void Clear()
    {
        _weaponSlot.Clear();
        _armorSlot.Clear();
        _shieldSlot.Clear();
        _helmetSlot.Clear();
    }

    private void SwapEquipment(ItemSlot equipSlot, Item newItem)
    {
        Item previousItem = equipSlot.ItemInSlot;

        // Экипируем новый предмет
        equipSlot.SetItem(newItem);

        // Если в слоте уже был предмет, возвращаем его в инвентарь
        if (previousItem != null)
        {
            _inventory.AddItem(previousItem);
        }
    }
}
