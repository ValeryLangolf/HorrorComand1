using System;
using UnityEngine;

public class HintTriggerDetector : RayDetector
{
    private Transform _transform;
    private bool _isDetected;

    public event Action<HintTrigger> Detected;
    public event Action Undetected;

    public HintTriggerDetector(float rayDistance) : base(Camera.main.transform, rayDistance)
    {
        _transform = Camera.main.transform;
    }

    protected override void HandleHits()
    {
        int hitCount = GetHitCount(_transform.forward, out RaycastHit[] hits);
        bool isHit = IsHit(hits, hitCount, out HintTrigger hintTrigger);

        if (_isDetected == isHit)
            return;

        _isDetected = isHit;

        if (_isDetected)
            Detected?.Invoke(hintTrigger);
        else
            Undetected?.Invoke();
    }

    private bool IsHit(RaycastHit[] hits, int hitCount, out HintTrigger hintTrigger)
    {
        hintTrigger = null;

        for (int i = 0; i < hitCount; i++)
            if (hits[i].collider.TryGetComponent(out hintTrigger))
                return true;

        return false;
    }
}