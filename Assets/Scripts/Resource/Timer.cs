using System;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] private float _resourceRespawnTime;
    [SerializeField] private float _spawnerRespawnTime;

    public static event Action spawnResource;
    public static event Action spawnSpawner;

    private float _resourceTimer;
    private float _spawnerTimer;

    private void Start()
    {
        _resourceTimer = Time.time;
        _spawnerTimer = _resourceTimer;
    }

    private void Update()
    {
        if (Time.time > _resourceTimer + _resourceRespawnTime && spawnResource != null)
        {
            AddResourceTime();
            spawnResource();
        }
        if (Time.time > _spawnerTimer + _spawnerRespawnTime)
        {
            AddSpawnerTime();
            spawnSpawner();
        }
    }

    private void AddResourceTime()
    {
        _resourceTimer += _resourceRespawnTime;
    }

    private void AddSpawnerTime()
    {
        _spawnerTimer += _spawnerRespawnTime;
    }
}