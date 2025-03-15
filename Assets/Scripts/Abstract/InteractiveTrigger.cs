using UnityEngine;

[RequireComponent(typeof(Collider))]
public class InteractiveTrigger : Entity
{
    [SerializeField] private GameInteractions _name;

    private Collider _triggerCollider;
    public GameInteractions Name => _name;

    private void Awake() =>
        _triggerCollider = GetComponent<Collider>();

    public void EnableCollider() =>
        _triggerCollider.enabled = true;

    public void DisableCollider() =>
        _triggerCollider.enabled = false;
}
