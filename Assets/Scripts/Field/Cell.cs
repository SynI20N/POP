using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Cell : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Color _glowColor;

    private GameObject _cell;
    private Material _cellMaterial;

    public static event Action<Cell> onPointerClick;
    private void Start()
    {
        _cell = gameObject;
        _cellMaterial = _cell.GetComponent<MeshRenderer>().material;
        _cellMaterial.EnableKeyword("_EMISSION");
    }
    public void OnPointerClick(PointerEventData eventData)
    {

        LightCell();
    }

    private void LightCell()
    {
        SetColor(_glowColor);
    }

    private void UnlightCell()
    {
        SetColor(_glowColor);
    }

    private Color GetColor()
    {
        return _cellMaterial.GetColor("_EmissionColor");
    }

    private void SetColor(Color glowColor)
    {
        _cellMaterial.SetColor("_EmissionColor", glowColor);
    }
}
