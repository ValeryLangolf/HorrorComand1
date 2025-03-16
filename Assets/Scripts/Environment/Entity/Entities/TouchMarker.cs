using UnityEngine;

[RequireComponent(typeof(Collider))]
public abstract class TouchMarker : Entity
{
    private Collider _collider;

    protected virtual void Awake() =>
        _collider = GetComponent<Collider>();

    public void EnableCollider() =>
        _collider.enabled = true;

    public void DisableCollider() =>
        _collider.enabled = false;

    protected void SetIsTriggerCollider() =>
        _collider.isTrigger = true;

    protected void SetIsSolidCollider() =>
        _collider.isTrigger = false;
}