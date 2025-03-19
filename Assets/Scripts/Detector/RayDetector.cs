using System;
using System.Collections;
using UnityEngine;

public abstract class RayDetector
{
    private readonly RaycastHit[] _hits = new RaycastHit[5];
    private readonly MonoBehaviour _monoBehaviour;
    private readonly Transform _transform;
    private readonly float _distance;

    private Coroutine _coroutine;

    public RayDetector(Transform transform, float rayDistance)
    {
        _monoBehaviour = transform.GetComponent<MonoBehaviour>();
        _transform = transform;
        _distance = rayDistance;
    }

    public void Enable()
    {
        Disable();
        _coroutine = _monoBehaviour.StartCoroutine(DetectOverTime());
    }

    public void Disable()
    {
        if (_coroutine != null)
            _monoBehaviour.StopCoroutine(_coroutine);
    }

    protected abstract void HandleHits();

    protected int GetHitCount(Vector3 direction, out RaycastHit[] hits)
    {
        Ray ray = new(_transform.position, direction);
        int hitCount = Physics.RaycastNonAlloc(ray, _hits, _distance);
        hits = _hits;

        return hitCount;
    }

    private IEnumerator DetectOverTime()
    {
        while (true)
        {
            HandleHits();

            yield return null;
        }
    }
}