using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class Inventory : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private int _maxSlots;

    private List<Item> _itemList;

    private List<Item> _chosenItems;

    public static event Action<Inventory> onInventoryOpen;

    protected virtual void Start()
    {
        _chosenItems = new List<Item>();
        _itemList = new List<Item>();
    }

    private void ChooseItems(Item item, int mode)
    {
        if (mode == 0)
        {
            var chosenItems =
                from invItem in _itemList
                where invItem.GetImage() == item.GetImage() &&
                !invItem.Amount.IsFull()
                select invItem;
            _chosenItems = new List<Item>(chosenItems);
        }
        else
        {
            var chosenItems =
                from invItem in _itemList
                where invItem.GetImage() == item.GetImage()
                select invItem;
            _chosenItems = new List<Item>(chosenItems);
        }    
    }

    private void PolishInventory(Item item)
    {
        ChooseItems(item, 0);

        if (_chosenItems.Count() == 2)
        {
            RecountItems();           
        }
    }

    private void RecountItems()
    {
        Item firstItem = _chosenItems.ElementAt(0);
        Item secondItem = _chosenItems.ElementAt(1);

        int seccondAmount = secondItem.Amount.GetAmount();

        int amountRemain = firstItem.Amount.Increase(seccondAmount );
        secondItem.Amount.Decrease(seccondAmount);
        if (amountRemain > 0)
        {
            secondItem.Amount.Increase(amountRemain);
        }
        else
        {
            _itemList.Remove(secondItem);
        }
    }

    private void RemoveLastItem(int removeAmount)
    {
        Item lastItem = _chosenItems.Last();
        int diffAmount = lastItem.Amount.Decrease(removeAmount);
        if (diffAmount > 0)
        {
            _itemList.Remove(lastItem);
            _chosenItems.ToList().Remove(lastItem);

            lastItem = _chosenItems.Last();
            lastItem.Amount.Decrease(diffAmount);
        }
    }

    private void RemoveSingleItem(int removeAmount)
    {
        Item singleItem = _chosenItems.First();
        if (singleItem.Amount.GetAmount() > removeAmount)
        {
            singleItem.Amount.Decrease(removeAmount);
        }
        else if (singleItem.Amount.GetAmount() == removeAmount)
        {
            _itemList.Remove(singleItem);
        }
    }

    private void TryRemove(Item removeItem)
    {
        int removeAmount = removeItem.Amount.GetAmount();
        int allAmount = 0;
        foreach (Item item in _chosenItems)
        {
            allAmount += item.Amount.GetAmount();
        }

        if (_chosenItems.Count() > 1 && allAmount >= removeAmount)
        {
            RemoveLastItem(removeAmount);
        }
        else if (_chosenItems.Count() == 1 && allAmount >= removeAmount)
        {
            RemoveSingleItem(removeAmount);
        }
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
        }

        PolishInventory(item);
    }

    public void RemoveItem(Item item)
    {
        if (item == null)
        {
            return;
        }

        ChooseItems(item, 1);

        TryRemove(item);

        Destroy(item.gameObject);
    }

    public List<Item> GetItemList()
    {
        return new List<Item>(_itemList);
    }

    public void OnPointerClick(PointerEventData pointerEvent)
    {
        onInventoryOpen.Invoke(this);
    }
}