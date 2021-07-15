using UnityEngine;

public class ResourceSpawner : MonoBehaviour
{
    private GameObject _resourcePrefab;

    private const uint _initHeight = 10;
    private const float _initSize = 0.35f;
    private void Start()
    {
        Timer.spawnResource += CreateResource;
        _resourcePrefab = Resources.Load<GameObject>("Prefabs/Resource");
    }

    private void CreateResource()
    {
        GameObject resource = Instantiate(_resourcePrefab);

        resource.transform.localScale = new Vector3(_initSize, _initSize, _initSize);
        resource.transform.position = new Vector3(resource.transform.position.x,
                                                  _initHeight,
                                                  resource.transform.position.z);
    }
}