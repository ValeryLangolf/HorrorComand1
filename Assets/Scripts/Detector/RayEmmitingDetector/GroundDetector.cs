using UnityEngine;

public class GroundDetector
{
    private readonly RaycastHit[] _hits = new RaycastHit[5];
    private readonly Transform _transform;
    private readonly Vector3 _direction = Vector3.down;
    private readonly float _distance;

    public GroundDetector(Transform transform, float rayDistance)
    {
        _transform = transform;
        _distance = rayDistance;
    }

    public bool IsGrounded()
    {
        Ray ray = new(_transform.position, _direction);
        int hitCount = Physics.RaycastNonAlloc(ray, _hits, _distance);

        for (int i = 0; i < hitCount; i++)
            if (_hits[i].collider.TryGetComponent(out Player _) == false)
                return true;

        return false;
    }
}