using UnityEngine;

public class CameraVerticalRotator : MonoBehaviour
{
    private const string MouseY = "Mouse Y";

    [SerializeField] private float _sensitivity = 2f;
    [SerializeField] private float _minimumVerticalAngle = - 80;
    [SerializeField] private float _maximumVerticalAngle = 65;

    private float currentAngleX = 0f;
    private bool _isRotate;

    public void EnableRotate() =>
        _isRotate = true;

    public void DisableRotate() =>
        _isRotate = false;

    private void LateUpdate()
    {
        if (_isRotate == false)
            return;

        currentAngleX -= Input.GetAxis(MouseY) * _sensitivity;
        currentAngleX = Mathf.Clamp(currentAngleX, _minimumVerticalAngle, _maximumVerticalAngle);

        Quaternion rotation = Quaternion.Euler(currentAngleX, 0, 0);
        transform.rotation = rotation;
    }
}