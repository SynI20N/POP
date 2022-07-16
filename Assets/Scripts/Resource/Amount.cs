using System;
using UnityEngine;
using static Unity.Mathematics.math;

[Serializable]
public class Amount
{
    [SerializeField] private int _currentAmount;
    [SerializeField] private int _maxAmount = 100;

    public Amount(int initAmount)
    {
        _currentAmount = initAmount;
    }

    public Amount(Amount original)
    {
        _currentAmount = original.Value();
    }

    public int Decrease(int amount)
    {
        if (!Assert(amount, 0, _currentAmount))
        {
            amount -= _currentAmount;
            _currentAmount = 0;
            return amount;
        }
        _currentAmount -= amount;
        return 0;
    }

    public int Increase(int amount)
    {
        if (!Assert(amount, 0, _maxAmount - _currentAmount))
        {
            amount -= _maxAmount - _currentAmount;
            _currentAmount = _maxAmount;
            return amount;
        }
        _currentAmount += amount;
        return 0;
    }

    private bool Assert(int value, int min, int max)
    {
        int clampedValue = clamp(value, min, max);
        if (clampedValue != value)
        {
            return false;
        }
        return true;
    }

    public int Value()
    {
        return _currentAmount;
    }

    public bool IsFull()
    {
        if (_currentAmount >= _maxAmount)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void SetMax(int maxAmount)
    {
        _maxAmount = maxAmount;
    }
}