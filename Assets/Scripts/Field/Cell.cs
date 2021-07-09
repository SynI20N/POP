using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Cell : MonoBehaviour, ICellObjects, ISpawnable, IPointerClickHandler
{
    private List<GameObject> _objects = new List<GameObject>();

    public static event Action<float, float> onClick;
    public static event Action<Cell> onPointerClick;

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
        onClick.Invoke(gameObject.transform.position.x,
                       gameObject.transform.position.z);
        onPointerClick(this);
    }
}
