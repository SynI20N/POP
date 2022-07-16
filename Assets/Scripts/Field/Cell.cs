using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[Serializable]
[JsonObject(MemberSerialization.OptIn)]
public class Cell : MonoBehaviour, IPointerClickHandler
{
    public static event Action<Inventory> OnClickInventory;
    public static event Action<Cell> OnClick;

    //serialized
    [JsonProperty] private Inventory _inventory;
    [JsonProperty] private bool _isPassable = true;
    [JsonProperty] private List<Transform> _objects;

    private void Start()
    {
        _inventory = GetComponent<Inventory>();
        _objects = new List<Transform>();

        UpdateContents();
    }

    public void UpdateContents()
    {
        Item item;
        foreach (Transform child in transform)
        {
            if (!_objects.Contains(child))
            {
                _objects.Add(child);
                if (child.gameObject.TryGetComponent(out item))
                {
                    _inventory.AddItem(item);
                }
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
        OnClickInventory(_inventory);
        OnClick(this);
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    public List<Item> GetItems()
    {
        return _inventory.GetItemList();
    }
}
