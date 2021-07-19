using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class Inventory : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private int _maxSlots;

    private List<Item> _itemList;

    public static event Action<Inventory> onCharacterClick;

    private void Start()
    {
        _itemList = new List<Item>();
    }

    private void PolishInventory(Item item)
    {
        var chosenItems =
          from invItem in _itemList
          where invItem.GetImage() == item.GetImage() && 
          !invItem.Amount.IsFull()
          select invItem;
        if (chosenItems.Count() == 2)
        {
            RecountAddedItems(chosenItems);           
        }
    }

    private void RecountAddedItems(IEnumerable<Item> chosenItems)
    {
        Item firstItem = chosenItems.First();
        Item secondItem = chosenItems.Last();

        int seccondAmount = secondItem.Amount.GetAmount();

        int amountRemain = firstItem.Amount.Increase(seccondAmount);
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

    private void RemoveLastItem(IEnumerable<Item> chosenItems, int removeAmount)
    {
        Item lastItem = chosenItems.Last();
        int diffAmount = lastItem.Amount.Decrease(removeAmount);
        if (diffAmount > 0)
        {
            _itemList.Remove(lastItem);
            chosenItems.ToList().Remove(lastItem);

            lastItem = chosenItems.Last();
            lastItem.Amount.Decrease(diffAmount);
        }
    }

    private void RemoveSingleItem(IEnumerable<Item> chosenItems, int removeAmount)
    {
        Item singleItem = chosenItems.First();
        if (singleItem.Amount.GetAmount() > removeAmount)
        {
            singleItem.Amount.Decrease(removeAmount);
        }
        else if (singleItem.Amount.GetAmount() == removeAmount)
        {
            _itemList.Remove(singleItem);
        }
    }

    private void TryRemove(Item removeItem, IEnumerable<Item> chosenItems)
    {
        int removeAmount = removeItem.Amount.GetAmount();
        int allAmount = 0;
        foreach (Item item in chosenItems)
        {
            allAmount += item.Amount.GetAmount();
        }

        if (chosenItems.Count() > 1 && allAmount > removeAmount)
        {
            RemoveLastItem(chosenItems, removeAmount);
        }
        else if (chosenItems.Count() == 1 && allAmount > removeAmount)
        {
            RemoveSingleItem(chosenItems, removeAmount);
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
            Destroy(item);
        }

        PolishInventory(item);
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

        TryRemove(item, chosenItems);       
    }

    public List<Item> GetItemList()
    {
        return _itemList;
    }

    public void OnPointerClick(PointerEventData pointerEvent)
    {
        onCharacterClick.Invoke(this);
    }
}