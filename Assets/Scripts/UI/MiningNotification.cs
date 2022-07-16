using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using System;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public class MiningNotification : MonoBehaviour
{
    [SerializeField] private ThirdPersonCamera _camera;
    [SerializeField] private Image _image;
    [SerializeField] private TextMeshProUGUI _text;

    private GameObject _notification;
    private float tweenTime = 1f;

    private void Start()
    {
        _notification = gameObject;
        Miner.OnMinedResource += throwNotification;
    }

    private void OnDestroy()
    {
        Miner.OnMinedResource -= throwNotification;
    }

    private void throwNotification(ItemNotification info)
    {
        _notification.GetComponent<RectTransform>().anchoredPosition = _camera.WorldToScreenPoint(info.Position);
        _text.text = "+" + info.Item.Amount.Value();
        _image.sprite = info.Item.GetImage().sprite;
        _notification.GetComponent<CanvasGroup>().DOFade(1f, tweenTime);
        _notification.transform.DOLocalMoveY(transform.localPosition.y + 50f, tweenTime);
        StartCoroutine(nameof(HideNotification));
    }

    private IEnumerator HideNotification()
    {
        yield return new WaitForSeconds(tweenTime);

        _notification.GetComponent<CanvasGroup>().DOFade(0f, tweenTime);
    }
}
