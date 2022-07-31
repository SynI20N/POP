using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;
using System;

[Serializable]
[JsonObject(MemberSerialization.OptIn)]
public class Item : MonoBehaviour
{
    [SerializeField] private Image _image;

    [JsonProperty] [SerializeField] public Amount Amount;

    public Image GetImage()
    {
        return _image;
    }

    public void SetImage(Image image)
    {
        _image = image;
    }
}