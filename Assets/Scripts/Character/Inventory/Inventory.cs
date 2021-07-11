using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory: MonoBehaviour
{
    [SerializeField] private UI_Inventory _uiInventory;

    private List<InventoryItem> _itemList;

    public event EventHandler OnItemListChanged;

    private void Awake()
    {
        _uiInventory.SetInventory(this);
    }
    public Inventory()
    {
        _itemList = new List<InventoryItem>();
    }

    public void AddItem(InventoryItem item) 
    {
        if(item.isStackable())
        {
            bool itemAlreadyInInventory = false;
            foreach (InventoryItem inventoryItem in _itemList)
            {
                if(inventoryItem.GetItemType() == item.GetItemType())
                {
                    inventoryItem.AddAmount(item.GetAmount());
                    itemAlreadyInInventory = true;
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

    public void RemoveItem(InventoryItem item)
    {
        if (item.isStackable())
        {
            InventoryItem itemInInventory = null;
            foreach (InventoryItem inventoryItem in _itemList)
            {
                if (inventoryItem.GetItemType() == item.GetItemType())
                {
                    inventoryItem.DecreaseAmount(item.GetAmount());
                    itemInInventory = inventoryItem;
                }
            }
            if (itemInInventory != null && itemInInventory.GetAmount() <= 0)
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

    public List<InventoryItem> GetItemList()
    {
        return _itemList;
    }
}
