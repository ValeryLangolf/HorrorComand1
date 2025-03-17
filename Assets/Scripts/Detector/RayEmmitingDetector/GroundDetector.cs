using System;
using System.Collections;
using UnityEngine;

public class GroundDetector
{
    private readonly RaycastHit[] _hits = new RaycastHit[5];
    private readonly Transform _transform;
    private readonly Vector3 _direction = Vector3.down;
    private readonly float _distance;
    private readonly MonoBehaviour _monoBehaviour;
    private Coroutine _coroutine;

    public event Action DownJumped;

    public GroundDetector(Transform transform, float rayDistance)
    {
        _monoBehaviour = transform.GetComponent<MonoBehaviour>();
        _transform = transform;
        _distance = rayDistance;
    }

    public bool IsGrounded { get; private set; }

    public Entity GroundMarker { get; private set; }

    public void Enable()
    {
        Disable();
        _coroutine = _monoBehaviour.StartCoroutine(DetectGroundOverTime());
    }

    public void Disable()
    {
        if (_coroutine != null)
            _monoBehaviour.StopCoroutine(_coroutine);
    }

    private bool IsGroundCollision(out Collider collider)
    {
        collider = null;

        Ray ray = new(_transform.position, _direction);
        int hitCount = Physics.RaycastNonAlloc(ray, _hits, _distance);

        for (int i = 0; i < hitCount; i++)
            if (_hits[i].collider.TryGetComponent(out Player _) == false)
            {
                collider = _hits[i].collider;
                return true;
            }

        return false;
    }

    private void ReadCollision()
    {
        if (IsGroundCollision(out Collider collider) == false)
        {
            IsGrounded = false;
            return;
        }

        if (collider.TryGetComponent(out GroundMarker groundMarker))
            GroundMarker = groundMarker;


        if (IsGrounded)
            return;

        IsGrounded = true;
        DownJumped?.Invoke();
    }

    private IEnumerator DetectGroundOverTime()
    {
        while (true)
        {
            ReadCollision();

            yield return null;
        }
    }
}