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
