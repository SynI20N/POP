using DG.Tweening;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using static UnityEngine.Vector3;

public class InventoryPanel : MonoBehaviour
{
    [SerializeField] private int _slotsInWidth;

    private const float _tweenTime = 0.5f;
    private const float _startOpenSize = 0.75f;

    private GameObject _panel;

    private Inventory _inventory;
    private Transform _itemSlotContainer;
    private Transform _itemSlotTemplate;

    private float _itemSlotSize;
    private float _itemSlotOffset = 50f;

    private CanvasGroup _canvasGroup;

    private void Start()
    {
        _panel = gameObject;
        _canvasGroup = _panel.GetComponent<CanvasGroup>();
        _canvasGroup.blocksRaycasts = false;

        _itemSlotContainer = transform.Find("ItemSlotContanier");
        _itemSlotTemplate = _itemSlotContainer.Find("ItemSlotTemplate");

        SetSlotSize();

        ExtendedStart();
    }

    protected virtual void ExtendedStart()
    {

    }

    private void OnDestroy()
    {
        cleanContainer();
        if (_inventory != null)
        {
            _inventory.OnDirty -= updateInventory;
        }

        ExtendedDestroy();
    }

    protected virtual void ExtendedDestroy()
    {

    }

    public void loadInventory(Inventory inventory)
    {
        if (_inventory != null)
        {
            _inventory.OnDirty -= updateInventory;
        }
        _inventory = inventory;
        _inventory.OnDirty += updateInventory;

        updateInventory();

        Animate(1f);
    }

    private void cleanContainer()
    {
        foreach (Transform child in _itemSlotContainer)
        {
            if (child != _itemSlotTemplate)
            {
                Destroy(child.gameObject);
            }
        }
    }

    private void updateInventory()
    {
        cleanContainer();

        refreshInventoryItems();
    }

    private void refreshInventoryItems()
    {
        int x = 0;
        int y = 0;
        foreach (Item item in _inventory.GetItemList())
        {
            RectTransform itemSlotRectTransform = CreatRectTransform();
            itemSlotRectTransform.gameObject.SetActive(true);

            SetPosition(itemSlotRectTransform, x, y);
            UnityEngine.UI.Image image = FindImage(itemSlotRectTransform);
            image.sprite = item.GetImage().sprite;

            TextMeshProUGUI uiText = FindText(itemSlotRectTransform);
            SetText(uiText, item);

            x++;
            if (x > _slotsInWidth - 1)
            {
                x = 0;
                y++;
            }
        }
    }

    private RectTransform CreatRectTransform()
    {
        return Instantiate(_itemSlotTemplate, _itemSlotContainer).GetComponent<RectTransform>();
    }

    private void SetPosition(RectTransform itemSlotRectTransform, int x, int y)
    {
        itemSlotRectTransform.anchoredPosition = new Vector2(x * _itemSlotSize + _itemSlotOffset, -y * _itemSlotSize);
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
        if (item.Amount.Value() > 1)
        {
            uiText.text = ("x" + item.Amount.Value().ToString());
        }
        else
        {
            uiText.text = ("");
        }
    }

    private void SetSlotSize()
    {
        RectTransform panelRT = _panel.GetComponent<RectTransform>();
        float backgroundWidth = panelRT.rect.width;

        _itemSlotSize = backgroundWidth / (float)_slotsInWidth;

        RectTransform slotRT = _itemSlotTemplate.GetComponent<RectTransform>();
        slotRT.DOScale(slotRT.localScale * _itemSlotSize / slotRT.rect.width, 0);

        _itemSlotOffset *= _itemSlotSize / slotRT.rect.width;
    }

    private void Animate(float alpha)
    {
        _canvasGroup.alpha = alpha;
        _canvasGroup.blocksRaycasts = true;
        _panel.transform.localScale = one * _startOpenSize;
        _panel.transform.DOKill();
        _panel.transform.DOScale(alpha, _tweenTime);
    }
    public void Close()
    {
        Animate(0f);
    }
}