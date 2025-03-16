using System;
using System.Collections;
using UnityEngine;

public class HintTriggerDetector
{
    private readonly MonoBehaviour _monoBehaviour;
    private readonly Transform _transform;
    private readonly float _distance;
    private readonly RaycastHit[] _hits = new RaycastHit[5];

    private Coroutine _coroutine;
    private bool _isDetected;

    public event Action<HintTrigger> Detected;
    public event Action Undetected;

    public HintTriggerDetector(float rayDistance)
    {
        _transform = Camera.main.transform;
        _monoBehaviour = _transform.GetComponent<MonoBehaviour>();
        _distance = rayDistance;
    }

    public void StartDetection()
    {
        StopDetection();
        _coroutine = _monoBehaviour.StartCoroutine(DetectOverTime());
    }

    public void StopDetection()
    {
        if (_coroutine != null)
            _monoBehaviour.StopCoroutine(_coroutine);
    }

    private void PerformIntersectionCheck()
    {
        Ray ray = new(_transform.position, _transform.forward);
        int hitCount = Physics.RaycastNonAlloc(ray, _hits, _distance);

        for (int i = 0; i < hitCount; i++)
            if (IsFound(_hits[i]))
                return;

        if (_isDetected)
        {
            _isDetected = false;
            Undetected?.Invoke();
        }
    }

    private bool IsFound(RaycastHit hit)
    {
        bool isFound = hit.collider.TryGetComponent(out HintTrigger hintTrigger);

        if (isFound == true && _isDetected == false)
        {
            _isDetected = true;
            Detected?.Invoke(hintTrigger);
        }

        return isFound;
    }

    private IEnumerator DetectOverTime()
    {
        while (true)
        {
            PerformIntersectionCheck();

            yield return null;
        }
    }
}