using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Inventory))]
public class Miner : MonoBehaviour
{
    public static event Action<ItemNotification> OnMinedResource;

    [SerializeField] private List<Item> _minedItems;
    [SerializeField] private float _miningTime;

    private Amount _progress;
    private Inventory _storage;
    private bool _mining;
    private Collider _meshCollider;

    private void Start()
    {
        _storage = GetComponentInChildren<Inventory>();

        _mining = false;
        _meshCollider = GetComponent<Collider>();
        _progress = new Amount(0);
        _progress.SetMax(100);
    }

    private void OnTriggerStay(Collider other)
    {
        Item item;
        if(!_mining && objectIsItem(other, out item))
        {
            foreach(var i in _minedItems)
            {
                if(i.GetImage() == item.GetImage())
                {
                    _mining = true;
                    StartCoroutine(nameof(Mining), item);
                    break;
                }
            }
        }
    }

    private bool objectIsItem(Collider someCollider, out Item item)
    {
        Inventory someInventory;
        bool isItem = someCollider.TryGetComponent(out item);
        bool isInventoryItem = someCollider.TryGetComponent(out someInventory);
        if(isItem && !isInventoryItem)
        {
            return true;
        }
        return false;
    }

    private IEnumerator Mining(Item item)
    {
        _progress = new Amount(0);
        while (!_progress.IsFull())
        {
            checkFar(item);
            _progress.Increase(10);
            yield return new WaitForSeconds(_miningTime / 10f);
        }
        OnMinedResource(new ItemNotification(item, transform.position));
        Inventory inventory = item.GetComponentInParent<Inventory>();

        inventory.RemoveItem(item);
        _storage.AddItem(item);

        Destroy(item.gameObject);
        _mining = false;
    }

    private void checkFar(Item someItem)
    {
        Collider other = someItem.GetComponent<Collider>();
        if (!_meshCollider.bounds.Intersects(other.bounds))
        {
            _mining = false;
            StopCoroutine(nameof(Mining));
        }
    }
}
