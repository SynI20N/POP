using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : MonoBehaviour
{
    [SerializeField] private int _resourceAmount;
    private GameObject _resource;   

    private void Start()
    {
        _resource = gameObject;
    }
}
