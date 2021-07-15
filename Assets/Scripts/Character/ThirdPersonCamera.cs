using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    [SerializeField] private Camera _camera;

    private Transform _objectTransform;
    private Vector3 _linkVector;

    private void Start()
    {
        if (_camera == null)
        {
            return;
        }

        _objectTransform = GetComponent<Transform>();
        _linkVector = _camera.transform.position - _objectTransform.position;
    }

    private void Update()
    {
        _camera.transform.position = _objectTransform.position + _linkVector;
    }
}
