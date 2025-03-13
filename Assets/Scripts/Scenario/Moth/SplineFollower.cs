using SplineMesh;
using System.Collections;
using UnityEngine;

public class SplineFollower : MonoBehaviour
{
    private const float TimeFly = 2f;

    [SerializeField] private Spline _spline;
    [SerializeField] private float _speed;

    private float _splineRate = 0f;
    private float _elapsedTime;
    private Coroutine _coroutine;

    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out Player player))
            if (_coroutine == null)
                _coroutine = StartCoroutine(MoveOverTime());
    }

    private void Move()
    {
        _splineRate += _speed * Time.deltaTime;

        if (_splineRate <= _spline.nodes.Count - 1)
            Place();
    }

    private void Place()
    {
        CurveSample sample = _spline.GetSample(_splineRate);

        transform.localPosition = sample.location;
        transform.localRotation = sample.Rotation;
    }

    private IEnumerator MoveOverTime()
    {
        _elapsedTime = TimeFly;

        while (_elapsedTime > 0)
        {
            yield return null;

            Move();
            _elapsedTime -= Time.deltaTime;
        }

        _coroutine = null;
    }
}