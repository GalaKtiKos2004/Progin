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
    [SerializeField] private CanvasGroup _canvasGroup;
    
    private Item _selectedItem;
    private ItemSlot _selectedItemSlot;
    private bool _isActive = false;

    private void OnEnable()
    {
        _useButton.onClick.AddListener(UseItem);
        _dropButton.onClick.AddListener(DropItem);
        _canvasGroup.alpha = 0;
        _canvasGroup.blocksRaycasts = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            _isActive = !_isActive;
            
            _canvasGroup.alpha = _isActive ? 1 : 0;
            _canvasGroup.blocksRaycasts = _isActive;
        }
    }

    public void SelectItem(ItemSlot item)
    {
        _selectedItemSlot = item;
        _selectedItem = item.ItemInSlot;
        _descriptionText.text = _selectedItem.Description;
    }

    private void UseItem()
    {
        if (_selectedItem != null)
        {
            _inventory.RemoveItemFromSlot(_selectedItemSlot);
            _equipmentManager.EquipItem(_selectedItem);
        }
    }


    private void DropItem()
    {
        Debug.Log("drop item");
        if (_selectedItem != null)
        {
            _inventory.RemoveItemFromSlot(_selectedItemSlot);
            _selectedItem = null;
            _descriptionText.text = "";
        }
    }

}
