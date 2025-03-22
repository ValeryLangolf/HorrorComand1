using UnityEngine;

[RequireComponent(typeof(Collider))]
public abstract class ObjectCollider : Entity
{
    private Collider _collider;

    private void Awake() =>
        _collider = GetComponent<Collider>();

    public void EnableCollider() =>
        _collider.enabled = true;

    public void DisableCollider() =>
        _collider.enabled = false;

    public void SetColliderAsTrigger() =>
        _collider.isTrigger = true;

    public void SetColliderAsSolid() =>
        _collider.isTrigger = false;
}