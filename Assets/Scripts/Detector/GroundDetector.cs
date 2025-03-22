using System;
using System.Collections;
using UnityEngine;

public class GroundDetector : MonoBehaviour
{
    [SerializeField] private LayerMask _ignoreLayer;
    [SerializeField] private float _distance;

    private readonly Vector3 _direction = Vector3.down;
    private bool _isInAir;
    private Coroutine _coroutine;

    public event Action Landed;

    private void Start() =>
        Enable();

    private void Enable()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(DetectingLanding());
    }

    public bool IsGrounded()
    {
        Ray ray = new(transform.position, _direction);

        return Physics.Raycast(ray, _distance, ~_ignoreLayer);
    }

    public GroundMarker GetGroundMarker()
    {
        Ray ray = new(transform.position, _direction);

        if (Physics.Raycast(ray, out RaycastHit hit, _distance, ~_ignoreLayer) && hit.collider.TryGetComponent(out GroundMarker groundMarker))
            return groundMarker;

        return null;
    }

    private IEnumerator DetectingLanding()
    {
        while (true)
        {
            ReadLanding();

            yield return null;
        }
    }

    private void ReadLanding()
    {
        bool isGrounded = IsGrounded();

        if (isGrounded == false)
            _isInAir = true;

        if (isGrounded == false || _isInAir == false)
            return;

        _isInAir = false;
        Landed?.Invoke();
    }
}