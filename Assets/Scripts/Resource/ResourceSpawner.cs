using UnityEngine;

public class ResourceSpawner : MonoBehaviour
{
    private Object _resourcePrefab;

    private int _resourceBoxCounter = 0;
    private void Start()
    {
        Timer.spawnResource += CreateResource;
        _resourcePrefab = Resources.Load<GameObject>("Prefabs/Resource");
    }

    private void CreateResource()
    {
        GameObject resource = Instantiate(_resourcePrefab) as GameObject;
        resource.transform.SetParent(gameObject.transform, false);
        resource.transform.localScale = new Vector3(0.35f, 0.35f, 0.35f);

        resource.transform.position = new Vector3(resource.transform.position.x,
                                                  _resourceBoxCounter * 1.2f,
                                                  resource.transform.position.z);

        _resourceBoxCounter += 1;
    }
}