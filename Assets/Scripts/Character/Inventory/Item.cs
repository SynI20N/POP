using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;
using System;
using System.Data;

[Serializable]
[JsonObject(MemberSerialization.OptIn)]
public class Item
{
    private Image _image;

    [JsonProperty] public Amount Amount { get; private set; }

    public Item(int currentAmount, int maxAmount, Image image)
    {
        Amount = new Amount(currentAmount, maxAmount);
        _image = image;
    }

    public string GetIconName()
    {
        return _image.name;
    }

    public Image GetImage()
    {
        return _image;
    }
}