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
        Item item;
        foreach (Transform child in transform)
        {
            bool result = child.gameObject.TryGetComponent(out item);
            if (result && !_objects.Contains(item))
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
