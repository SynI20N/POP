using UnityEngine;

public class Resource : Item
{
    private GameObject _resource;

    protected override void Start()
    {
        base.Start();
        _resource = gameObject;
    }
}
