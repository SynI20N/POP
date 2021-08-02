using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[Serializable]
[JsonObject(MemberSerialization.OptIn)]
public class Cell : Inventory, IPointerClickHandler
{
    public static event Action<Cell> OnClick;
    
    //serialized
    [JsonProperty] private List<Item> _items;
    [JsonProperty] private bool _isPassable = true;
    [JsonProperty] private List<Vector3> _objects;

    protected new void Start()
    {
        base.Start();
        _objects = new List<Vector3>();
        _items = new List<Item>();
        UpdateContents();      
    }

    public void UpdateContents()
    {
        ItemComponent itemComponent;
        foreach (Transform child in transform)
        {
            if (!_objects.Contains(child.transform.position))
            {
                _objects.Add(child.transform.position);
            }
            bool result = child.gameObject.TryGetComponent(out itemComponent);
            if (result && !_items.Contains(itemComponent.GetThisItem()))
            {
                Item item = itemComponent.GetThisItem();
                base.AddItem(item);
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

    public new void OnPointerClick(PointerEventData pointerEvent)
    {
        OnClick(this);
        base.OnPointerClick(pointerEvent);
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
