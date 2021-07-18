using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Cell : MonoBehaviour, ISpawnable, IPointerClickHandler
{
    public static event Action<Cell> onPointerClick;

    private List<Item> _objects = new List<Item>();
    private Transform _thisTransform;
    private bool _spawnAbility = true;

    private void Start()
    {
        _thisTransform = gameObject.transform;

        UpdateContents();
    }

    public void UpdateContents()
    {
        foreach (Transform child in transform)
        {
            Item item;
            if (child.gameObject.TryGetComponent(out item))
            {
                _objects.Add(item);
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

    public void OnPointerClick(PointerEventData pointerEvent)
    {
        onPointerClick(this);
    }

    public Vector3 GetPosition()
    {
        return _thisTransform.position;
    }

    public List<Item> GetObjects()
    {
        List<Item> result = new List<Item>(_objects);
        return result;
    }
}
