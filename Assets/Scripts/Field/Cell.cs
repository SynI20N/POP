using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Cell : MonoBehaviour, ISpawnable, IPointerClickHandler
{
    private List<GameObject> _objects = new List<GameObject>();

    public static event Action<Cell> onPointerClick;

    private Transform _thisTransform;
    private bool _spawnAbility = true;

    private void Start()
    {
        _thisTransform = gameObject.transform;
    }

    public void AddObject(GameObject gameObject)
    {
        _objects.Add(gameObject);
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

    public Transform GetTransform()
    {
        return _thisTransform;
    }
}
