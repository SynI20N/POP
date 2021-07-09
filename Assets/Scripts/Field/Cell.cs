using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Cell : MonoBehaviour, ICellObjects, ISpawnable, IPointerClickHandler
{
    private List<GameObject> _objects = new List<GameObject>();

    public static event Action<float,float> OnClick;

    private bool _spawnAbility = true;

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
        OnClick.Invoke(gameObject.transform.position.x,
                       gameObject.transform.position.z);
    }
}
