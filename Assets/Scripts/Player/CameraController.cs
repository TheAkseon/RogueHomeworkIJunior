using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private Transform _pivot;
    [SerializeField] private float _rotateSpeed;
    [SerializeField] private Vector3 _offset;

    private void Start()
    {
        _offset = _target.position - transform.position;

        _pivot.transform.position = _target.transform.position;
        _pivot.transform.parent = null;

        Cursor.lockState = CursorLockMode.Locked;
    }

    private void LateUpdate()
    {
        _pivot.transform.position = _target.transform.position;

        float horizontal = Input.GetAxis("Mouse X") * _rotateSpeed;
        _pivot.Rotate(0, horizontal, 0);

        float desiredYAngle = _pivot.eulerAngles.y;
        float desiredXAngle = _pivot.eulerAngles.x;
        Quaternion rotation = Quaternion.Euler(desiredXAngle, desiredYAngle, 0);
        transform.position = _target.position - (rotation * _offset);

        if (transform.position.y < _target.position.y)
        {
            transform.position = new Vector3(transform.position.x, _target.position.y - 0.5f, transform.position.z);
        }

        transform.LookAt(_target);
    }
}
