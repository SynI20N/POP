using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Character : MonoBehaviour, IPointerClickHandler
{
    public static event Action<Inventory> OnCharacterClick;

    private Inventory _inventory;
    private void Start()
    {
        _inventory = GetComponent<Inventory>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        OnCharacterClick(_inventory);
    }
}
