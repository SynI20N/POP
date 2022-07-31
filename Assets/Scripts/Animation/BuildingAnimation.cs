using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BuildingAnimation : MonoBehaviour
{
    [SerializeField] private Transform _crossSectionPlane;
    [SerializeField] private Transform _buildingCopy;

    private Material _buildingMaterial;
    private Material _buildingCopyMaterial;
    private BuildingStage _stage;
    private Sequence _sequence;

    public void Initiate(Transform someBuildingCopy)
    {
        _buildingMaterial = GetComponent<MeshRenderer>().sharedMaterial;
        _buildingCopyMaterial = someBuildingCopy.GetComponent<MeshRenderer>().sharedMaterial;
        _sequence = DOTween.Sequence();
    }

    public void SetBuildingStage(BuildingStage someBuildingStage)
    {
        _stage = someBuildingStage;
        _stage.SetSequence(_sequence);

        Build();
    }

    public void Build()
    {
        _stage.Build(_buildingMaterial, _buildingCopyMaterial);
    }
}
