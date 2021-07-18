using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
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
          where invItem.GetImage() == item.GetImage() && !invItem.Amount.IsFull()
          select invItem;
        if (chosenItems.Count() == 2)
        {
            int diffAmount = chosenItems.First().Amount.Increase(chosenItems.Last().Amount.GetAmount());
            chosenItems.Last().Amount.Decrease(chosenItems.Last().Amount.GetAmount());
            if(diffAmount > 0)
            {
                chosenItems.Last().Amount.Increase(diffAmount);
            }
            else
            {
                _itemList.Remove(chosenItems.Last());
            }          
        }
        else if (chosenItems.Count() == 1)
        {

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
            item.Destroy();
        }

        PolishInventory(item);
    }

    public void RemoveItem(Item item)
    {
        if(item == null)
        {
            return;
        }

        var chosenItems =
          from invItem in _itemList
          where invItem.GetImage() == item.GetImage()
          select invItem;

        if(chosenItems.Count() >= 2)
        {
            int diffAmount = chosenItems.Last().Amount.Decrease(item.Amount.GetAmount());
            if(diffAmount > 0)
            {
                _itemList.Remove(chosenItems.Last());
                chosenItems.ToList().Remove(chosenItems.Last());

                chosenItems.Last().Amount.Decrease(diffAmount);
            }
        }
        else if(chosenItems.Count() == 1)
        {
            if(chosenItems.First().Amount.GetAmount() > item.Amount.GetAmount())
            {
                chosenItems.First().Amount.Decrease(item.Amount.GetAmount());
            }
            else if (chosenItems.First().Amount.GetAmount() == item.Amount.GetAmount())
            {
                _itemList.Remove(chosenItems.First());
            }
        }
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