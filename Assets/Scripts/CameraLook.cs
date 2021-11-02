using UnityEngine;

public class CameraLook : MonoBehaviour
{
    [SerializeField] float _mouseSensitivity = 100f;
    [SerializeField] float _upLimit = -90f;
    [SerializeField] float _downLimit = 90f;

    private float _xRotation = 0f;
    private Transform _playerTransform;
    private void Start()
    {
        _playerTransform = transform.parent;
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void LateUpdate()
    {
        float mouseX = Input.GetAxis("Mouse X") * _mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * _mouseSensitivity * Time.deltaTime;

        _xRotation -= mouseY;
        _xRotation = Mathf.Clamp(_xRotation, _upLimit, _downLimit);

        transform.localRotation = Quaternion.Euler(_xRotation, 0, 0);
        _playerTransform.Rotate(Vector3.up * mouseX);
    }
}
