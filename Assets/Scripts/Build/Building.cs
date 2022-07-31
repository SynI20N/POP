using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    [SerializeField] private Transform _buildingCopy;

    private Transform _transform;
    private BuildingAnimation _animation;

    private void Start()
    {
        _transform = GetComponent<Transform>();

        Build();
    }

    private void Destruct()
    {
        _animation = gameObject.AddComponent<BuildingAnimation>();
        _animation.Initiate(_buildingCopy);
        _animation.SetBuildingStage(new CoolingStage());
        _animation.SetBuildingStage(new AssemblingStage());
        _animation.SetBuildingStage(new TemplateStage());

        Destroy(_animation);
    }

    private void Build()
    {
        _animation = gameObject.AddComponent<BuildingAnimation>();
        _animation.Initiate(_buildingCopy);
        _animation.SetBuildingStage(new TemplateStage());
        _animation.SetBuildingStage(new AssemblingStage());
        _animation.SetBuildingStage(new CoolingStage());

        Destroy(_animation);
    }
}
