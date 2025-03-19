using System.Collections;
using UnityEngine;

public class PositionStabilizator
{
    private readonly MonoBehaviour _monoBehaviour;
    private readonly Transform _transform;
    private readonly Transform _target;
    private readonly float _speed;
    private Coroutine _coroutine;

    public PositionStabilizator(Transform transform, Transform target, float speed)
    {
        _monoBehaviour = transform.GetComponent<MonoBehaviour>();
        _transform = transform;
        _target = target;
        _speed = speed;
    }

    public void Enable()
    {
        Disable();
        _coroutine = _monoBehaviour.StartCoroutine(StabilizeOverTime());
    }

    public void Disable()
    {
        if (_coroutine != null)
            _monoBehaviour.StopCoroutine(_coroutine);
    }

    private void Stabilize()
    {
        _transform.position = Vector3.MoveTowards(_transform.position, _target.position, _speed * Time.deltaTime);
    }

    private IEnumerator StabilizeOverTime()
    {
        while (true)
        {
            Stabilize();

            yield return null;
        }
    }
}