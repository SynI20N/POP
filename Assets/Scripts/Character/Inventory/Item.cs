using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{   
    [SerializeField] private Sprite _sprite;

    [SerializeField] private bool _isStackable;

    [SerializeField] private int _maxAmount;

    public Amount Amount { get; private set; }

    private void Start()
    {
        Amount.SetMax(_maxAmount);
    }

    public bool IsStackable()
    {
        return _isStackable;
    }

    public Sprite GetSprite()
    {
        return _sprite;
    }
}