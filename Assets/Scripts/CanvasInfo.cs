using UnityEngine;

public class CanvasInfo : MonoBehaviour
{
    Transform _transform;
    bool _rotationUpdated = false;
    Quaternion _rotateAngle;
    Vector3 _rotateVector = Vector3.forward * 5f + Vector3.right * 60f;
    private void Start()
    {
        _rotateAngle = Quaternion.Euler(_rotateVector);
        _transform = GetComponent<Transform>();
        _transform.rotation = _rotateAngle;
    }
    private void Update()
    {
        if (!_rotationUpdated) return;
        _transform.rotation = _rotateAngle;
    }
}
