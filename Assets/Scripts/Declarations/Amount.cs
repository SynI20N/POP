using System.Diagnostics;
using UnityEngine;
using static Unity.Mathematics.math;

public class Amount
{
    private int _currentAmount;
    private int _maxAmount;

    public Amount(int initAmount, int maxAmount)
    {
        _currentAmount = initAmount;
        _maxAmount = maxAmount;
    }

    public int Decrease(int amount)
    {
        if (!Assert(amount, 0, _currentAmount))
        {
            _currentAmount = 0;
            return amount - _currentAmount;
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

    public int GetAmount()
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
}