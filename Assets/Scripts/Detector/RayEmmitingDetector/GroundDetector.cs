using System;
using UnityEngine;

public class GroundDetector : RayDetector
{
    private readonly Vector3 _direction = Vector3.down;
    private bool _isUpJumped;

    public event Action DownJumped;

    public GroundDetector(Transform transform, float rayDistance) : base(transform, rayDistance) { }

    public bool IsGrounded()
    {
        int hitCount = GetHitCount(_direction, out RaycastHit[] hits);

        for (int i = 0; i < hitCount; i++)
            if (hits[i].collider.TryGetComponent(out Player _) == false)
                return true;

        return false;
    }

    public Entity GetGroundMarker()
    {
        int hitCount = GetHitCount(_direction, out RaycastHit[] hits);

        for (int i = 0; i < hitCount; i++)
            if (hits[i].collider.TryGetComponent(out GroundMarker groundMarker))
                return groundMarker;

        return null;
    }

    protected override void HandleHits()
    {
        bool isGrounded = IsGrounded();

        if (isGrounded == false)
        {
            _isUpJumped = true;
            return;
        }

        if (isGrounded &&  _isUpJumped)
        {
            _isUpJumped = false;
            DownJumped?.Invoke();
        }
    }
}