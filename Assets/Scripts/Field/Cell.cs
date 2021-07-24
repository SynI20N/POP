using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Cell : Inventory, IPointerClickHandler
{
    public static event Action<Cell> onPointerClick;

    private Transform _thisTransform;
    private bool _spawnAbility = true;

    protected new void Start()
    {
        base.Start();
        _thisTransform = gameObject.transform;
        UpdateContents();      
    }

    public void UpdateContents()
    {
        Item item;
        foreach (Transform child in transform)
        {
            bool result = child.gameObject.TryGetComponent(out item);
            if (result && !base.GetItemList().Contains(item))
            {
                base.AddItem(item);
            }
        }
    }

    public bool CheckSpawn()
    {
        return _spawnAbility;
    }

    public void SetAbility(bool ability)
    {
        _spawnAbility = ability;
    }

    public new void OnPointerClick(PointerEventData pointerEvent)
    {
        onPointerClick(this);
        base.OnPointerClick(pointerEvent);
    }

    public Vector3 GetPosition()
    {
        return _thisTransform.position;
    }
}
