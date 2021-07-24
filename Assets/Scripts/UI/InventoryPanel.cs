using DG.Tweening;
using TMPro;
using UnityEngine;
using static UnityEngine.Vector3;

public class InventoryPanel : MonoBehaviour
{
    [SerializeField] private int _iconsInLenght;
    [SerializeField] private float _offset;

    private const float _tweenTime = 0.5f;
    private const float _startOpenSize = 0.6f;
    
    private CanvasGroup _canvasGroup;
    private GameObject _panel;
    private Transform _itemSlotContainer;

    private Inventory _inventory;
    
    private Vector3 _nextPos;

    private void Start()
    {
        _panel = gameObject;
        _canvasGroup = _panel.GetComponent<CanvasGroup>();

        _itemSlotContainer = transform.Find("ItemSlotContanier");

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
        _nextPos = new Vector3(50f + _offset, -50f, 0);

        _inventory = inventory;

        SpawnHelper.ClearChildrenIn(_itemSlotContainer);

        RefreshInventoryItems();

        Animate(1f);
    }

    private void RefreshInventoryItems()
    {
        foreach (Item item in _inventory.GetItemList())
        {
            GameObject icon = CreateIcon(item);
            //itemSlotRectTransform.gameObject.SetActive(true);
            
            TextMeshProUGUI uiText = FindText(icon);
            SetText(uiText, item);

            SetPosition(icon);
            CalculateNextPos(icon);     
        }
    }

    private GameObject CreateIcon(Item item)
    {
        return Instantiate(item.GetImage().gameObject, _itemSlotContainer);
    }

    private void CalculateNextPos(GameObject icon)
    {
        float width = icon.GetComponent<RectTransform>().rect.width;
        float height = icon.GetComponent<RectTransform>().rect.height;
        
        _nextPos += right * (width + _offset);
        if (_nextPos.x > _iconsInLenght * (width + _offset))
        {
            _nextPos += down * (height + _offset);
            _nextPos.x = 50f + _offset;
        }
    }

    private void SetPosition(GameObject icon)
    {
        icon.GetComponent<RectTransform>().anchoredPosition = _nextPos;
    }

    private TextMeshProUGUI FindText(GameObject icon)
    {
        return icon.GetComponentInChildren<TextMeshProUGUI>();
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