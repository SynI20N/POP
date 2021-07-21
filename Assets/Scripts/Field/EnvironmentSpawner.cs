using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Vector3;

public class EnvironmentSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> _environmentPrefabs;

    private Vector3 _spawnPos;
    private Quaternion _rotation;
    private RaycastHit _hit;
    private int _counter;

    public void SpawnEnvironment(Cell cell)
    {
        int count = Random.Range(0, _environmentPrefabs.Count + 1);
        int rotation = Random.Range(0, 360);
        for (_counter = 0; _counter < count; _counter++)
        {
            CalculatePos(cell);

            GameObject obj = Instantiate(_environmentPrefabs[_counter], _spawnPos, _rotation);
            obj.transform.eulerAngles += new Vector3(0, rotation, 0);
            obj.transform.SetParent(cell.transform);
        }
    }

    private void CalculatePos(Cell cell)
    {
        int layerMask = 1 << 0;
        _spawnPos = SpawnHelper.PosInsideCircle(cell.gameObject);
        Physics.Raycast(_spawnPos, down, out _hit, 100f, layerMask);
        PosOnGround();
    }

    private void PosOnGround()
    {
        float upScale = _environmentPrefabs[_counter].transform.localScale.z / 2;
        _spawnPos = _hit.point;
        _rotation = Quaternion.FromToRotation(up, _hit.normal);
    }
}
