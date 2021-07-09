using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] private float _respawnTime;

    public static event Action spawnResource;

    private float _timer;

    private void Start()
    {
        _timer = Time.time;
    }

    private void Update()
    {
        if (Time.time > _timer + _respawnTime)
        {
            AddTime();
            spawnResource();
        }
    }

    private void AddTime()
    {
        _timer += _respawnTime;
    }
}
