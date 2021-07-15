using DG.Tweening;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEngine.Vector3;

public class CellPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textDescription;
    [SerializeField] private RectTransform _firstItemPoint;

    private const float _tweenTime = 0.5f;
    private const float _startOpenSize = 0.6f;

    private Stack<GameObject> _icons;
    private Vector3 _nextIconPos;
    private GameObject _panel;
    private CanvasGroup _canvasGroup;

    private void Start()
    {
        _panel = gameObject;
        _canvasGroup = _panel.GetComponent<CanvasGroup>();
        _nextIconPos = zero;
        _icons = new Stack<GameObject>(20);

        Cell.onPointerClick += Open;
    }

    private void OnDestroy()
    {
        Cell.onPointerClick -= Open;
    }

    public void Close()
    {
        Animate(0f);
    }

    public void Open(Cell cell)
    {
        Animate(1f);
        ClearStack();
        DisplayItems(cell);
    }

    private void DisplayItems(Cell cell)
    {
        List<GameObject> contents = cell.GetObjects();
        foreach (var c in contents)
        {
            Item item;
            if (c.TryGetComponent(out item))
            {
                GameObject image = Instantiate(item.GetImage().gameObject, _firstItemPoint);
                image.GetComponent<RectTransform>().localPosition = _nextIconPos;
                _icons.Push(image);
                _nextIconPos += right;
            }
        }
    }

    private void ClearStack()
    {
        for (int i = 0; i < _icons.Count; i++)
        {
            Destroy(_icons.Pop());
        }
    }

    private void Animate(float alpha)
    {
        _canvasGroup.alpha = alpha;
        _panel.transform.localScale = one * _startOpenSize;
        _panel.transform.DOKill();
        _panel.transform.DOScale(alpha, _tweenTime);
    }
}
