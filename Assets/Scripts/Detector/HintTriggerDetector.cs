using System;
using System.Collections;
using UnityEngine;

public class HintTriggerDetector : MonoBehaviour
{
    [SerializeField] private float _distance;
    [SerializeField] private LayerMask _ignoreLayer;

    private bool _isDetected;
    private Coroutine _coroutine;

    public event Action<HintTrigger> Detected;
    public event Action Undetected;

    private void Start() => 
        Enable();

    private void Enable()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(DetectOverTime());
    }

    private IEnumerator DetectOverTime()
    {
        while (true)
        {
            ReadHintTrigger();

            yield return null;
        }
    }

    private void ReadHintTrigger()
    {
        Ray ray = new(transform.position, transform.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, _distance, ~_ignoreLayer) == false)
            return;

        bool isHintTrigger = hit.collider.TryGetComponent(out HintTrigger hintTrigger);
        HandleState(isHintTrigger, hintTrigger);
    }

    private void HandleState(bool isHintTrigger, HintTrigger hintTrigger)
    {
        if (_isDetected == isHintTrigger)
            return;

        _isDetected = isHintTrigger;

        if (_isDetected)
            Detected?.Invoke(hintTrigger);
        else
            Undetected?.Invoke();
    }
}