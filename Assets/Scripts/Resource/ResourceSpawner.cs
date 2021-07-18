using System.Collections;
using UnityEngine;
using static UnityEngine.Random;

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
        GameObject resource = Instantiate(_resourcePrefab, PosInsideCircle(gameObject), Quaternion.identity);
        _thisCell.AddObject(resource);
        StartCoroutine("DelayedRest", resource);
        _resourceCount++;
    }

    public static Vector3 PosInsideCircle(GameObject someObject)
    {
        Vector3 result = new Vector3();
        result.x = someObject.transform.position.x;
        result.y = _initHeight;
        result.z = someObject.transform.position.z;

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