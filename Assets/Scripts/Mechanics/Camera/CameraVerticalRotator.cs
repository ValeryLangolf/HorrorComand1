using UnityEngine;

public class CameraVerticalRotator
{
    private const string MouseY = "Mouse Y";

    private const float MinimumVerticalAngle = - 80;
    private const float MaximumVerticalAngle = 65;

    private readonly Transform _transform;
    private readonly float _sensitivity;
    private float _currentAngleX = 0f;

    public CameraVerticalRotator(Transform transform, float sensitivity)
    {
        _transform = transform;
        _sensitivity = sensitivity;
    }

    public void Rotate()
    {
        _currentAngleX -= Input.GetAxis(MouseY) * _sensitivity;
        _currentAngleX = Mathf.Clamp(_currentAngleX, MinimumVerticalAngle, MaximumVerticalAngle);
        float currentAngleY = _transform.eulerAngles.y;

        Quaternion rotation = Quaternion.Euler(_currentAngleX, currentAngleY, 0);
        _transform.rotation = rotation;
    }
}