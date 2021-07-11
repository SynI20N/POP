using static Unity.Mathematics.math;

public class Amount
{
    private int _currentAmount;
    private int _maxAmount = 100;

    public void Decrease(int amount)
    {
        if(!Assert(amount, 0, _currentAmount))
        {
            return;
        }
        _currentAmount -= amount;
    }

    public int Increase(int amount)
    {
        if (!Assert(amount, 0, _maxAmount - _currentAmount))
        {
            _currentAmount = _maxAmount;
            return amount - (_maxAmount - _currentAmount);
        }
        _currentAmount += amount;
        return 0;
    }

    private bool Assert(int value, int min, int max)
    {
        int clampedValue = clamp(value, min, max);
        if(clampedValue != value)
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
        if(_currentAmount >= _maxAmount)
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