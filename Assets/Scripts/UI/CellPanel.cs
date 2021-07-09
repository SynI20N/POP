using DG.Tweening;
using System;
using TMPro;
using UnityEngine;
using static UnityEngine.Vector3;

public class CellPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textDescription;

    private const float _tweenTime = 0.5f;

    private GameObject _panel;
    private CanvasGroup _canvasGroup;

    public static event Action<float> onExit;

    private void Start()
    {
        _panel = gameObject;
        _canvasGroup = _panel.GetComponent<CanvasGroup>();

        Cell.onPointerClick += Open;
    }

    private void OnDestroy()
    {
        Cell.onPointerClick -= Open;
    }

    public void Close()
    {
        Animate(0f);
        onExit.Invoke(-1f);
    }

    public void Open(Cell cell)
    {
        _textDescription.text = cell.gameObject.name;

        Animate(1f);
    }

    private void Animate(float alpha)
    {
        _canvasGroup.alpha = alpha;
        _panel.transform.localScale = zero;
        _panel.transform.DOKill();
        _panel.transform.DOScale(alpha, _tweenTime);
    }
}
