using System.Collections;
using UnityEngine;

public class HorizontalRotator
{
    private const string MouseX = "Mouse X";

    private readonly MonoBehaviour _monoBehaviour;
    private readonly Transform _transform;
    private readonly float _sensitivity;
    private float _currentAngleY = 0f;
    private Coroutine _coroutine;

    public HorizontalRotator(Transform transform, float sensitivity)
    {
        _monoBehaviour = transform.GetComponent<MonoBehaviour>();
        _transform = transform;
        _sensitivity = sensitivity;
    }

    public void Enable()
    {
        Disable();
        _currentAngleY = _transform.eulerAngles.y + Input.GetAxis(MouseX) * _sensitivity;
        _coroutine = _monoBehaviour.StartCoroutine(RotateOverTime());
    }

    public void Disable()
    {
        if (_coroutine != null)
            _monoBehaviour.StopCoroutine(_coroutine);
    }

    private IEnumerator RotateOverTime()
    {
        while (true)
        {
            Rotate();

            yield return null;
        }
    }

    private void Rotate()
    {
        _currentAngleY += Input.GetAxis(MouseX) * _sensitivity;
        Quaternion rotation = Quaternion.Euler(0, _currentAngleY, 0);
        _transform.rotation = rotation;
    }
}