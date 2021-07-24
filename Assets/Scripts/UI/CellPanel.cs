using DG.Tweening;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Vector3;

public class CellPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textDescription;
    [SerializeField] private RectTransform _firstItemPoint;
    [SerializeField] private uint _itemsPerRow;

    private const float _tweenTime = 0.5f;
    private const float _openSize = 0.6f;

    private Stack<Item> _items;
    private Vector3 _nextIconPos;
    private GameObject _panel;
    private CanvasGroup _canvasGroup;

    private void Start()
    {
        _panel = gameObject;
        _canvasGroup = _panel.GetComponent<CanvasGroup>();
        _nextIconPos = zero;
        _items = new Stack<Item>();

        Cell.OnClick += Open;
    }

    private void OnDestroy()
    {
        Cell.OnClick -= Open;
    }

    public void Close()
    {
        Animate(0f);
    }

    public void Open(Cell cell)
    {
        ResfreshStack();
        DisplayItems(cell);
        Animate(1f);
    }

    private void DisplayItems(Cell cell)
    {
        List<Item> contents = cell.GetItems();
        GameObject image;
        foreach (var c in contents)
        {
            _items.Push(c);
            image = Instantiate(c.GetImage().gameObject, _firstItemPoint);
            image.GetComponent<RectTransform>().localPosition = _nextIconPos;

            CalculateNextPos(c.GetImage());
        }
    }

    private void CalculateNextPos(Image image)
    {
        float width = image.sprite.rect.width;
        float height = image.sprite.rect.height;

        _nextIconPos += right * width;
        if (_nextIconPos.x >= _itemsPerRow * width)
        {
            _nextIconPos += down * height;
            _nextIconPos.x = 0;
        }
    }

    private void ResfreshStack()
    {
        _nextIconPos = zero;
        Image[] images = _firstItemPoint.GetComponentsInChildren<Image>();
        for (int i = 0; i < images.Length; i++)
        {
            _items.Pop();
            Destroy(images[i].gameObject);
        }
    }

    private void Animate(float alpha)
    {
        _canvasGroup.alpha = alpha;
        _panel.transform.localScale = one * _openSize;
        _panel.transform.DOKill();
        _panel.transform.DOScale(alpha, _tweenTime);
    }
}
