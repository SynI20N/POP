using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItem
{   
    private ItemType _itemType;
    
    private int _amount;
    
    public ItemType GetItemType()
    {
        return _itemType;
    }

    public bool isStackable()
    {
        switch(_itemType)
        {
            default:
            case ItemType.Ore:
            case ItemType.Metall:
                return true;
            case ItemType.Pistol:
                return false;
        }
    }

    public void AddAmount(int amount)
    {
        if(amount < 0)
        {
            Debug.Log("TyDolban?");
        }
        else
        {
            _amount += amount;
        }  
    }

    public void DecreaseAmount(int amount)
    {
        if (amount < 0)
        {
            Debug.Log("TyDolban?");
        }
        else if(_amount - amount > 0)
        {
            _amount -= amount;
        }
        else
        {
            Debug.Log("Idi uchi matematiku");
        }
    }

    public int GetAmount()
    {
        return _amount;
    }

    public Sprite GetSprite()
    {
        switch (_itemType)
        {
            default:
            case ItemType.Ore :     return ItemAssets.Instance.oreSprite;
            case ItemType.Metall:   return ItemAssets.Instance.metallSprite;
            case ItemType.Pistol:   return ItemAssets.Instance.pistolSprite;
        }
    }
}