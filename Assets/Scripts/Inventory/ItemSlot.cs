using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
[RequireComponent(typeof(Button))]
public class ItemSlot : MonoBehaviour
{
    [SerializeField] private InventoryUi _inventoryUi;
    
    private Item _item = null;
    private Image _image;
    private Button _button;
    
    public Item ItemInSlot => _item;
    public bool CanSetItem => _item == null;

    private void Awake()
    {
        _image = GetComponent<Image>();
        _button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(OnClick);
    }

    public void SetItem(Item item)
    {
        _item = item;
        _image.sprite = item.icon;
        _image.enabled = true;
    }

    public void Clear()
    {
        _item = null;
        _image.sprite = null;
        _image.enabled = false;
    }

    public void OnClick()
    {
        if (_item != null)
        {
            _inventoryUi.SelectItem(this);
        }
    }
}
