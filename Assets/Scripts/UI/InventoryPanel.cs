using DG.Tweening;
using TMPro;
using UnityEngine;
using static UnityEngine.Vector3;

public class InventoryPanel : MonoBehaviour
{
    private const float _tweenTime = 0.5f;
    private const float _startOpenSize = 0.6f;

    private GameObject _panel;

    private Inventory _inventory;
    private Transform _itemSlotContainer;
    private Transform _itemSlotTemplate;

    private CanvasGroup _canvasGroup;

    private Vector3 _nextPos;

    private void Start()
    {
        _panel = gameObject;
        _canvasGroup = _panel.GetComponent<CanvasGroup>();

        _itemSlotContainer = transform.Find("ItemSlotContanier");
        _itemSlotTemplate = _itemSlotContainer.Find("ItemSlotTemplate");

        _nextPos = new Vector3(50f, 50f, 0);

        Inventory.onInventoryOpen += LoadInventory;
    }

    private void OnDestroy()
    {
        SpawnHelper.ClearChildrenIn(_itemSlotContainer);
        Inventory.onInventoryOpen -= LoadInventory;
    }

    private void LoadInventory(Inventory inventory)
    {
        _nextPos = new Vector3(50f, 50f, 0);

        _inventory = inventory;

        CleanContainer();

        RefreshInventoryItems();

        Animate(1f);
    }

    private void CleanContainer()
    {
        foreach (Transform child in _itemSlotContainer)
        {
            if (child != _itemSlotTemplate)
            {
                Destroy(child.gameObject);
            }
        }
    }

    private void RefreshInventoryItems()
    {
        foreach (Item item in _inventory.GetItemList())
        {
            RectTransform itemSlotRectTransform = CreatRectTransform();
            itemSlotRectTransform.gameObject.SetActive(true);
            
            TextMeshProUGUI uiText = FindText(itemSlotRectTransform);
            SetText(uiText, item);

            UnityEngine.UI.Image image = FindImage(itemSlotRectTransform);
            image.sprite = item.GetImage().sprite;

            SetPosition(itemSlotRectTransform);
            CalculateNextPos(itemSlotRectTransform);     
        }
    }

    private RectTransform CreatRectTransform()
    {
        return Instantiate(_itemSlotTemplate, _itemSlotContainer).GetComponent<RectTransform>();
    }

    private void CalculateNextPos(RectTransform itemSlotRectTransform)
    {
        float width = itemSlotRectTransform.rect.width;
        float height = itemSlotRectTransform.rect.height;

        _nextPos += right * width;
        if (_nextPos.x > 10 * width)
        {
            _nextPos += down * height;
            _nextPos.x = 0;
        }
    }
    private UnityEngine.UI.Image FindImage(RectTransform itemSlotRectTransform)
    {
        return itemSlotRectTransform.Find("ItemImage").GetComponent<UnityEngine.UI.Image>();
    }

    private void SetPosition(RectTransform itemSlotRectTransform)
    {
        itemSlotRectTransform.anchoredPosition = new Vector2(_nextPos.x, _nextPos.y);
    }

    private TextMeshProUGUI FindText(RectTransform itemSlotRectTransform)
    {
        return itemSlotRectTransform.Find("Amount").GetComponent<TextMeshProUGUI>();
    }

    private void SetText(TextMeshProUGUI uiText, Item item)
    {
        if (item.Amount.GetAmount() > 1)
        {
            uiText.SetText("x" + item.Amount.GetAmount().ToString());
        }
        else
        {
            uiText.SetText("");
        }
    }

    private void Animate(float alpha)
    {
        _canvasGroup.alpha = alpha;
        _panel.transform.localScale = one * _startOpenSize;
        _panel.transform.DOKill();
        _panel.transform.DOScale(alpha, _tweenTime);
    }
    public void Close()
    {
        Animate(0f);
    }
}