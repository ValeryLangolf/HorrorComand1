using UnityEngine;

[RequireComponent(typeof(Collider))]
public abstract class ColliderObject : MonoBehaviour
{
    private Collider _collider;

    private void Awake() =>
        _collider = GetComponent<Collider>();

    public void EnableCollider() =>
        _collider.enabled = true;

    public void DisableCollider() =>
        _collider.enabled = false;
}