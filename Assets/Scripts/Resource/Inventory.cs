using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

[Serializable]
[JsonObject(MemberSerialization.OptIn)]
public class Inventory : MonoBehaviour
{
    [JsonProperty] [SerializeField] private int _maxSlots;

    [JsonProperty] private List<Item> _itemList;
    public Action OnDirty;

    private void Start()
    {
        _itemList = new List<Item>();
    }

    private void PolishInventory(Item item)
    {
        var chosenItems =
           from invItem in _itemList
           where invItem.GetImage() == item.GetImage() && !invItem.Amount.IsFull()
           select invItem;
        if (chosenItems.Count() == 2)
        {
            int diffAmount = chosenItems.First().Amount.Increase(chosenItems.Last().Amount.Value());
            chosenItems.Last().Amount.Decrease(chosenItems.Last().Amount.Value() - diffAmount);
            if (diffAmount == 0)
            {
                _itemList.Remove(chosenItems.Last());
                Destroy(item);
            }
        }
        OnDirty?.Invoke();
    }

    internal bool Contains(Item item)
    {
        foreach(var i in _itemList)
        {
            if (i.Equals(item))
            {
                return true;
            }
        }
        return false;
    }

    public void AddItem(Item item)
    {
        if (item == null)
        {
            return;
        }

        if (_itemList.Count() < _maxSlots)
        {
            Item copiedItem = SpawnHelper.CopyComponent(item, gameObject);
            _itemList.Add(copiedItem);
            PolishInventory(copiedItem);
        }
    }

    public void RemoveItem(Item item)
    {
        if (item == null)
        {
            return;
        }

        var chosenItems =
          from invItem in _itemList
          where invItem.GetImage() == item.GetImage()
          select invItem;
        if (chosenItems.Count() >= 2)
        {
            int diffAmount = chosenItems.Last().Amount.Decrease(item.Amount.Value());
            if (diffAmount > 0)
            {
                _itemList.Remove(chosenItems.Last());
                chosenItems.ToList().Remove(chosenItems.Last());

                chosenItems.Last().Amount.Decrease(diffAmount);
                Destroy(chosenItems.Last());
            }
        }
        else if (chosenItems.Count() == 1)
        {
            if (chosenItems.First().Amount.Value() > item.Amount.Value())
            {
                chosenItems.First().Amount.Decrease(item.Amount.Value());
            }
            else if (chosenItems.First().Amount.Value() == item.Amount.Value())
            {
                Destroy(chosenItems.First());
                _itemList.Remove(chosenItems.First());
            }
        }
        OnDirty?.Invoke();
    }

    public List<Item> GetItemList()
    {
        return _itemList;
    }
}