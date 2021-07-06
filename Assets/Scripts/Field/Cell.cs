using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class Cell : MonoBehaviour, IPointerClickHandler, ILightable
{
    [SerializeField] private Color _glowColor;

    private GameObject _cell;
    private Material _cellMaterial;

    private Color _defaultColor;

    public static event Action<Cell> onPointerClick;
    private void Start()
    {
        _cell = gameObject;
        _cellMaterial = _cell.GetComponent<MeshRenderer>().material;
        _cellMaterial.EnableKeyword("_EMISSION");
        _defaultColor = GetColor();
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        onPointerClick.Invoke(this);
        Light();
    }

    public void Light()
    {
        SetColor(_glowColor);
    }

    public void Unlight()
    {
        SetColor(_defaultColor);
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
