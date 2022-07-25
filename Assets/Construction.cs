using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Construction : MonoBehaviour
{
    [SerializeField] private Transform _crossSectionPlane;
    [SerializeField] private Transform _buildingCopy;

    private Material _buildingMaterial;
    private Material _buildingCopyMaterial;

    private void Start()
    {
        _buildingMaterial = GetComponent<MeshRenderer>().sharedMaterial;
        _buildingCopyMaterial = _buildingCopy.GetComponent<MeshRenderer>().sharedMaterial;

        StartCoroutine(nameof(Construct));
    }

    private IEnumerator Construct()
    {
        while (true)
        {
            yield return new WaitForSeconds(2);
            _buildingMaterial.DOFloat(2f, "_PercentFinished", 10);
            _buildingCopyMaterial.DOFloat(2f, "_PercentFinished", 10);
            yield return new WaitUntil(() => _buildingMaterial.GetFloat("_PercentFinished") == 2f);
            _buildingMaterial.DOFloat(10f, "_PercentFinished", 2);
            _buildingCopyMaterial.DOFloat(10f, "_PercentFinished", 2);
            yield return new WaitUntil(() => _buildingMaterial.GetFloat("_PercentFinished") == 10f);
            _buildingMaterial.DOFloat(-0.3f, "_PercentFinished", 1);
            _buildingCopyMaterial.DOFloat(-0.3f, "_PercentFinished", 1);
        }
    }


}
