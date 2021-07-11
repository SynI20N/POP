using DG.Tweening;
using System;
using TMPro;
using UnityEngine;
using static UnityEngine.Vector3;
using System.Collections.Generic;

public class CellPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textDescription;

    private const float _tweenTime = 0.5f;

    //private Stack<TextMeshProUGUI> _contents;
    private GameObject _panel;
    private CanvasGroup _canvasGroup;

    private void Start()
    {
        _panel = gameObject;
        _canvasGroup = _panel.GetComponent<CanvasGroup>();
        //_contents = new Stack<TextMeshProUGUI>(10);

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
/*        List<GameObject> contents = cell.GetObjects();

        foreach(var c in contents)
        {
            TextMeshProUGUI text = new TextMeshProUGUI();
            text.text = c.name;
            if (!_contents.Contains(text))
            {
                _contents.Push(text);
            }
        }*/
    }

    private void Animate(float alpha)
    {
        _canvasGroup.alpha = alpha;
        _panel.transform.localScale = zero;
        _panel.transform.DOKill();
        _panel.transform.DOScale(alpha, _tweenTime);
    }
}
