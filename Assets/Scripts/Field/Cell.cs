using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Cell : MonoBehaviour, ICellObjects, ISpawnable
{
    private List<GameObject> _objects = new List<GameObject>();

    private bool _spawnAbility = true;

    private void Start()
    {
        
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
}
