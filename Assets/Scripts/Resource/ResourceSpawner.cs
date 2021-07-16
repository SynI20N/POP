using UnityEngine;
using static UnityEngine.Random;
using System.Collections;

[RequireComponent(typeof(Cell))]
public class ResourceSpawner : MonoBehaviour
{
    private GameObject _resourcePrefab;
    private Cell _thisCell;

    private const uint _initHeight = 5;
    private const uint _maxResourceCount = 10;
    private const float _initRadius = 0.75f;
    
    private uint _resourceCount;
    private void Start()
    {
        Timer.spawnResource += CreateResource;
        _resourcePrefab = Resources.Load<GameObject>("Prefabs/Resource");
        _thisCell = GetComponent<Cell>();
    }

    private void CreateResource()
    {
        if (_resourceCount > _maxResourceCount)
        {
            Timer.spawnResource -= CreateResource;
            StopAllCoroutines();
            Destroy(this);
        }
        GameObject resource = Instantiate(_resourcePrefab, PosInsideCircle(), Quaternion.identity);
        _thisCell.AddObject(resource);
        StartCoroutine("DelayedRest", resource);
        _resourceCount++;
    }

    private Vector3 PosInsideCircle()
    {
        Vector3 result = new Vector3();
        result.x = gameObject.transform.position.x;
        result.y = _initHeight;
        result.z = gameObject.transform.position.z;

        Vector2 insideCircle = insideUnitCircle * _initRadius;
        result.x += insideCircle.x;
        result.z += insideCircle.y;

        return result;
    }

    private IEnumerator DelayedRest(GameObject someObject)
    {
        yield return new WaitForSeconds(3);

        Destroy(someObject.GetComponent<Rigidbody>());
    }
}