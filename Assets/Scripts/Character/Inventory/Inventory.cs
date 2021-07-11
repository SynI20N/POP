using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using System.ComponentModel;

public class Inventory: MonoBehaviour
{
    [SerializeField] private UI_Inventory _uiInventory;
    [SerializeField] private int _maxSlotAmount;

    private List<Item> _itemList;

    public event EventHandler OnItemListChanged;

    private void Awake()
    {
        _uiInventory.SetInventory(this);
    }
    private void Start()
    {
        _itemList = new List<Item>();
    }

    private bool CanAdd(Item item)
    {
        if(_itemList.Count == _maxSlotAmount && !WillLastBeFull(item))
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    private bool WillLastBeFull(Item item)
    {
        var items = ChooseItems(item);
        return false;
    }

    private bool CanRemove(Item item)
    {
        return true;
    }

    private IEnumerable<Item> ChooseItems(Item item)
    {
        return from invItem in _itemList
               where invItem.GetSprite() == item.GetSprite()
               select invItem;
    }

    public void AddItem(Item item)
    {
        if(!CanAdd(item))
        {
            return;
        }
        if(item.IsStackable())
        {
            var choosedItems = from invItem in _itemList
                               where invItem.GetSprite() == item.GetSprite() &&
                               !invItem.Amount.IsFull()
                               select invItem; ;

            Item inventoryItem = choosedItems.FirstOrDefault();

            if (inventoryItem != null)
            {
                int diffAmount = inventoryItem.Amount.Increase(item.Amount.GetAmount());
                if (diffAmount > 0)
                {
                    item.Amount.DecreaseToZero();
                    item.Amount.Increase(diffAmount);

                    _itemList.Add(item);
                }
            }
            else
            {
                _itemList.Add(item);
            }
        }
        else
        {
            _itemList.Add(item);
        }
        OnItemListChanged.Invoke(this, EventArgs.Empty);
    }

    public void RemoveItem(Item item)
    {
        if (!CanRemove(item))
        {
            return;
        }
        if (item.IsStackable())
        {
            var choosedItems = ChooseItems(item);

            Item inventoryItem = choosedItems.LastOrDefault();

            if (inventoryItem != null)
            {
                int diffAmount = inventoryItem.Amount.Decrease(item.Amount.GetAmount());
                if (diffAmount > 0)
                {                  
                    _itemList.Remove(inventoryItem);
                    choosedItems.ToList().Remove(inventoryItem);

                    inventoryItem = choosedItems.LastOrDefault();
                    
                    item.Amount.DecreaseToZero();
                    item.Amount.Increase(diffAmount);
                    inventoryItem.Amount.Decrease(item.Amount.GetAmount());
                }
            }
            else
            {
                _itemList.Remove(item);
            }
        }
        else
        {
            _itemList.Remove(item);
        }
        OnItemListChanged.Invoke(this, EventArgs.Empty);
    }

    public List<Item> GetItemList()
    {
        return _itemList;
    }
}