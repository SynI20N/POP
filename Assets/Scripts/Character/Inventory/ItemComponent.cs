using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemComponent : MonoBehaviour
{
    [SerializeField] private Image _image;

    [SerializeField] private int _startAmount;
    [SerializeField] private int _maxAmount;

    private Item _thisItem;

    private void Start()
    {
        _thisItem = new Item(_startAmount, _maxAmount, _image);
    }

    public Image GetImage()
    {
        return _image;
    }

    public Item GetThisItem()
    {
        return _thisItem;
    }
}
