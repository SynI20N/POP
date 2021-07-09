using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceSpawner : MonoBehaviour
{
    [SerializeField] private static Resource _resourcePrefab;

    private void Start()
    {
        Timer.spawnResource += CreateResource;
    }

    private void CreateResource()//start on event
    {
        Resource resource = Instantiate<Resource>(_resourcePrefab);
        resource.transform.SetParent(gameObject.transform, false);
        resource.transform.localScale = new Vector3 (0.35f, 1f, 0.35f);
        Debug.Log("Resource spawned");
    }
}
