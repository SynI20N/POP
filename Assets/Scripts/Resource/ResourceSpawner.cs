using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Cell))]
public class ResourceSpawner : MonoBehaviour
{
    private GameObject _resourcePrefab;
    private Cell _thisCell;

    private const uint _maxResourceCount = 10;
    private uint _resourceCount;

    private void Start()
    {
        Timer.spawnResource += CreateResource;
        _resourcePrefab = Resources.Load<GameObject>("Prefabs/Iron");
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
        Vector3 spawnPos = SpawnHelper.PosInsideCircle(gameObject);
        GameObject resource = Instantiate(_resourcePrefab, spawnPos, Quaternion.identity);
        resource.transform.SetParent(_thisCell.transform);
        //_thisCell.UpdateContents();
        StartCoroutine("DelayedRest", resource);
        _resourceCount++;
    }

    private IEnumerator DelayedRest(GameObject someObject)
    {
        yield return new WaitForSeconds(3);

        Destroy(someObject.GetComponent<Rigidbody>());
    }
}