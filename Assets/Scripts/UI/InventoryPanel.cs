using TMPro;
using UnityEngine;

public class InventoryPanel : MonoBehaviour
{
    [SerializeField] private int _slotsInLenght;

    private Inventory _inventory;
    private Transform _itemSlotContanier;
    private Transform _itemSlotTemplate;
    private float _itemSlotSize;

    private void Awake()
    {
        _itemSlotContanier = transform.Find("ItemSlotContanier");
        _itemSlotTemplate = _itemSlotContanier.Find("ItemSlotTemplate");
        SetSlotSize();
    }

    public void SetInventory(Inventory inventory)
    {
        _inventory = inventory;
        inventory.OnItemListChanged += Inventory_OnItemListChanged;

    }

    private void Inventory_OnItemListChanged()
    {
        RefreshInventoryItems();
    }

    private void RefreshInventoryItems()
    {
        int x = 0;
        int y = 0;
        foreach (Item item in _inventory.GetItemList())
        {
            RectTransform itemSlotRectTransform = CreatRectTransform();
            itemSlotRectTransform.gameObject.SetActive(true);

            SetPosition(itemSlotRectTransform, x, y);
            if (x > _slotsInLenght)
            {
                x = 0;
                y++;
            }

            UnityEngine.UI.Image image = FindImage(itemSlotRectTransform);
            //image.sprite = item.GetImage();

            TextMeshProUGUI uiText = FindText(itemSlotRectTransform);
            SetText(uiText, item);
        }
    }

    private RectTransform CreatRectTransform()
    {
        return Instantiate(_itemSlotTemplate, _itemSlotContanier).GetComponent<RectTransform>();
    }

    private void SetPosition(RectTransform itemSlotRectTransform, int x, int y)
    {
        itemSlotRectTransform.anchoredPosition = new Vector2(x * _itemSlotSize, y * _itemSlotSize);
    }

    private UnityEngine.UI.Image FindImage(RectTransform itemSlotRectTransform)
    {
        return itemSlotRectTransform.Find("ItemImage").GetComponent<UnityEngine.UI.Image>();
    }

    private TextMeshProUGUI FindText(RectTransform itemSlotRectTransform)
    {
        return itemSlotRectTransform.Find("Amount").GetComponent<TextMeshProUGUI>();
    }

    private void SetText(TextMeshProUGUI uiText, Item item)
    {
        if (item.Amount.GetAmount() > 1)
        {
            uiText.SetText(item.Amount.GetAmount().ToString());
        }
        else
        {
            uiText.SetText("");
        }
    }

    private void SetSlotSize()
    {
        //atomaticly check and set SlotSize
    }
}
