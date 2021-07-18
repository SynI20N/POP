using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private int _maxAmount;

    public Amount Amount { get; private set; }

    private void Start()
    {
        Amount = new Amount(1);
        Amount.SetMax(_maxAmount);
    }

    public Image GetImage()
    {
        return _image;
    }
}