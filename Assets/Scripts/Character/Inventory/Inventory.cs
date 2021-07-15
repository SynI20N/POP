using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private InventoryPanel _inventoryPanel;
    [SerializeField] private int _maxSlots;

    private List<Item> _itemList;

    public event Action OnItemListChanged;

    private void Awake()
    {
        _inventoryPanel.SetInventory(this);
    }
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
            int diffAmount = chosenItems.First().Amount.Increase(chosenItems.Last().Amount.GetAmount());
        }
        else if (chosenItems.Count() == 1)
        {

        }
    }

    private bool CanRemove(Item item)
    {
        return true;
    }

    public void AddItem(Item item)
    {
        if (item == null)
        {
            return;
        }

        if (_itemList.Count() < _maxSlots)
        {
            _itemList.Add(item);
            item.Destroy();
        }

        PolishInventory(item);

        OnItemListChanged.Invoke();
    }


    public List<Item> GetItemList()
    {
        return _itemList;
    }
}