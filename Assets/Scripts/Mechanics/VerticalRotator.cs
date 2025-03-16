using System.Collections;
using UnityEngine;

public class VerticalRotator
{
    private const string MouseY = "Mouse Y";

    private const float MinimumVerticalAngle = - 80;
    private const float MaximumVerticalAngle = 65;

    private readonly MonoBehaviour _monoBehaviour;
    private readonly Transform _transform;
    private readonly float _sensitivity;
    private float _currentAngleX = 0f;
    private Coroutine _coroutine;

    public VerticalRotator(Transform transform, float sensitivity)
    {
        _monoBehaviour = transform.GetComponent<MonoBehaviour>();
        _transform = transform;
        _sensitivity = sensitivity;
    }

    public void Enable()
    {
        Disable();
        _currentAngleX = _transform.eulerAngles.x + Input.GetAxis(MouseY) * _sensitivity;
        _coroutine = _monoBehaviour.StartCoroutine(RotateOverTime());
    }

    public void Disable()
    {
        if (_coroutine != null)
            _monoBehaviour.StopCoroutine(_coroutine);
    }

    private void Rotate()
    {
        _currentAngleX -= Input.GetAxis(MouseY) * _sensitivity;
        _currentAngleX = Mathf.Clamp(_currentAngleX, MinimumVerticalAngle, MaximumVerticalAngle);
        float currentAngleY = _transform.eulerAngles.y;
        Quaternion rotation = Quaternion.Euler(_currentAngleX, currentAngleY, 0);
        _transform.rotation = rotation;
    }

    private IEnumerator RotateOverTime()
    {
        while (true)
        {
            Rotate();

            yield return null;
        }
    }
}