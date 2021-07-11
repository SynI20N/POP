using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private Sprite _sprite;

    [SerializeField] private int _maxAmount;

    public Amount Amount { get; private set; }

    private void Start()
    {
        Amount.SetMax(_maxAmount);
    }

    public Sprite GetSprite()
    {
        return _sprite;
    }

    public void Destroy()
    {

    }
}