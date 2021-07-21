using UnityEngine;

public class Resource : Item
{
    private GameObject _resource;

    private void Start()
    {
        _resource = gameObject;
    }
}
