using UnityEngine;

public class PlayerBodyHorizontalRotator
{
    private const string MouseX = "Mouse X";

    private readonly Transform _transform;
    private readonly float _sensitivity;
    private float _currentAngleY = 0f;

    public PlayerBodyHorizontalRotator(Transform transform, float sensitivity)
    {
        _transform = transform;
        _sensitivity = sensitivity;
    }

    public void Rotate()
    {
        _currentAngleY += Input.GetAxis(MouseX) * _sensitivity;

        Quaternion rotation = Quaternion.Euler(0, _currentAngleY, 0);
        _transform.rotation = rotation;
    }
}