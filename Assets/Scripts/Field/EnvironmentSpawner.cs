using System.Collections.Generic;
using UnityEngine;

public class EnvironmentSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> _environmentPrefabs;

    private Vector3 _spawnPos;

    public void SpawnEnvironment(Cell cell)
    {
        int count = Random.Range(0, _environmentPrefabs.Count);
        for(int i = 0; i < count; i++)
        {
            _spawnPos = ResourceSpawner.PosInsideCircle(cell.gameObject);
            cell.AddObject(Instantiate(_environmentPrefabs[i], _spawnPos, Quaternion.identity));
        }
    }
}
