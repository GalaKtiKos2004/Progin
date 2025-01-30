using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUi : MonoBehaviour
{
    [SerializeField] private Inventory _inventory;
    [SerializeField] private EqupimentManager _equipmentManager;
    [SerializeField] private Button _useButton;
    [SerializeField] private Button _dropButton;
    [SerializeField] private TextMeshProUGUI _descriptionText;
    [SerializeField] private Image _inventoryImage;
    
    private Item _selectedItem;
    private bool _isActive = false;

    private void OnEnable()
    {
        _useButton.onClick.AddListener(UseItem);
        _dropButton.onClick.AddListener(DropItem);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            _isActive = !_isActive;
            _inventoryImage.gameObject.SetActive(_isActive);
        }
    }

    public void SelectItem(Item item)
    {
        _selectedItem = item;
        _descriptionText.text = item.Description;
    }

    private void UseItem()
    {
        if (_selectedItem != null)
        {
            _inventory.RemoveItem(_selectedItem);
            _equipmentManager.EquipItem(_selectedItem);
        }
    }


    private void DropItem()
    {
        Debug.Log("drop item");
        if (_selectedItem != null)
        {
            _inventory.RemoveItem(_selectedItem);
            _selectedItem = null;
            _descriptionText.text = "";
        }
    }

}
