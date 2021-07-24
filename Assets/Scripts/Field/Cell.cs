using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[Serializable]
[JsonObject(MemberSerialization.OptIn)]
public class Cell : MonoBehaviour, IPointerClickHandler
{
    public static event Action<Cell> OnClick;
    
    //serialized
    [JsonProperty] private List<Item> _items = new List<Item>();
    [JsonProperty] private bool _isPassable = true;
    [JsonProperty] private List<Vector3> _objects;

    private void Start()
    {
        _objects = new List<Vector3>();

        UpdateContents();
    }

    public void UpdateContents()
    {
        Item item;
        foreach (Transform child in transform)
        {
            if (!_objects.Contains(child.transform.position))
            {
                _objects.Add(child.transform.position);
            }
            bool result = child.gameObject.TryGetComponent(out item);
            if (result && !_items.Contains(item))
            {
                _items.Add(item);
            }
        }
    }

    public bool CheckPassable()
    {
        return _isPassable;
    }

    public void SetAbility(bool ability)
    {
        _isPassable = ability;
    }

    public void OnPointerClick(PointerEventData pointerEvent)
    {
        OnClick(this);
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    public List<Item> GetItems()
    {
        List<Item> result = new List<Item>(_items);
        return result;
    }
}
