using UnityEngine;

public class InteractiveTrigger : MonoBehaviour
{
    [SerializeField] private GameInteractions _name;

    private Collider _triggerCollider;
    public GameInteractions Name => _name;

    private void Awake() =>
        _triggerCollider = GetComponent<Collider>();

    public void Enable() =>
        _triggerCollider.enabled = true;

    public void Disable() =>
        _triggerCollider.enabled = false;
}