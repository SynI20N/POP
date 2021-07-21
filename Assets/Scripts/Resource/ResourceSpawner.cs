using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Cell))]
public class ResourceSpawner : MonoBehaviour
{
    private GameObject _resourcePrefab;
    private Cell _thisCell;

    private const uint _maxResourceCount = 10;
    private const float _timeBeforeReveal = 2f;
    private uint _resourceCount;

    private void Start()
    {
        Timer.spawnResource += CreateResource;
        _resourcePrefab = Resources.Load<GameObject>("Prefabs/Iron");
        _thisCell = GetComponent<Cell>();
    }

    private void CreateResource()
    {
        CheckFull();
        GameObject resource = Spawn();
        _thisCell.UpdateContents();
        StartCoroutine(nameof(Reveal), resource);

        _resourceCount++;
    }

    private GameObject Spawn()
    {
        Vector3 spawnPos = SpawnHelper.PosInsideCircle(gameObject);
        GameObject resource = Instantiate(_resourcePrefab, spawnPos, Quaternion.identity);

        resource.GetComponent<MeshRenderer>().enabled = false;
        resource.transform.SetParent(_thisCell.transform);

        return resource;
    }

    private void CheckFull()
    {
        if (_resourceCount > _maxResourceCount)
        {
            Timer.spawnResource -= CreateResource;
            StopAllCoroutines();
            Destroy(this);
        }
    }

    private IEnumerator Reveal(GameObject someObject)
    {
        yield return new WaitForSeconds(_timeBeforeReveal);

        someObject.layer = 0;

        yield return new WaitForSeconds(_timeBeforeReveal / 5);

        Destroy(someObject.GetComponent<Rigidbody>());
        someObject.GetComponent<MeshRenderer>().enabled = true;
    }
}