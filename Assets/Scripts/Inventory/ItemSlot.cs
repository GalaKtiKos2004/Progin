using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Отвечает за работу слота предмета в инвентаре.
/// Позволяет хранить, отображать и взаимодействовать с предметами.
/// </summary>
[RequireComponent(typeof(Image))]
[RequireComponent(typeof(Button))]
public class ItemSlot : MonoBehaviour
{
    /// <summary> Ссылка на интерфейс инвентаря для взаимодействия. </summary>
    [SerializeField] private InventoryUi _inventoryUi;

    private Item _item = null;
    private Image _image;
    private Button _button;

    /// <summary> Возвращает предмет, находящийся в данном слоте. </summary>
    public Item ItemInSlot => _item;

    /// <summary> Определяет, можно ли добавить предмет в данный слот. </summary>
    public bool CanSetItem => _item == null;

    /// <summary>
    /// Инициализирует ссылки на компоненты.
    /// </summary>
    private void Awake()
    {
        _image = GetComponent<Image>();
        _button = GetComponent<Button>();
    }

    /// <summary>
    /// Подписывается на событие нажатия кнопки.
    /// </summary>
    private void OnEnable()
    {
        _button.onClick.AddListener(OnClick);
    }

    /// <summary>
    /// Устанавливает предмет в слот.
    /// </summary>
    /// <param name="item">Предмет, который нужно поместить в слот.</param>
    public void SetItem(Item item)
    {
        _item = item;
        _image.sprite = item.icon;
        _image.enabled = true;
    }

    /// <summary>
    /// Очищает слот, удаляя предмет.
    /// </summary>
    public void Clear()
    {
        _item = null;
        _image.sprite = null;
        _image.enabled = false;
    }

    /// <summary>
    /// Вызывается при нажатии на слот. Отправляет информацию о предмете в интерфейс инвентаря.
    /// </summary>
    public void OnClick()
    {
        if (_item != null)
        {
            _inventoryUi.SelectItem(this);
        }
    }
}
