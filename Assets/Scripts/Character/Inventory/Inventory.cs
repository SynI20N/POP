using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class Inventory: MonoBehaviour
{
    [SerializeField] private UI_Inventory _uiInventory;

    private List<Item> _itemList;

    public event EventHandler OnItemListChanged;

    private void Awake()
    {
        _uiInventory.SetInventory(this);
    }
    public void Start()
    {
        _itemList = new List<Item>();
    }

    public void AddItem(Item item)
    {
        if(item.IsStackable())
        {
            bool itemAlreadyInInventory = false;

            var choosedItems = from inventoryItem in _itemList 
                               where inventoryItem.GetSprite() == item.GetSprite()
                               select inventoryItem;

            foreach (Item inventoryItem in choosedItems)
            {
                if(!inventoryItem.Amount.IsFull())
                {
                    int diffAmount = inventoryItem.Amount.Increase(item.Amount.GetAmount());
                    if(diffAmount > 0)
                    {
                        itemAlreadyInInventory = true;
                    }
                    
                }
            }
            if(!itemAlreadyInInventory)
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
        if (item.IsStackable())
        {
            Item itemInInventory = null;
            foreach (Item inventoryItem in _itemList)
            {
                if (inventoryItem.GetSprite() == item.GetSprite())
                {
                    inventoryItem.Amount.Increase(item.Amount.GetAmount());
                    itemInInventory = inventoryItem;
                }
            }
            if (itemInInventory != null && itemInInventory.Amount.GetAmount() <= 0)
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
