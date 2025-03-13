using UnityEngine;

public class InteractiveTrigger : MonoBehaviour
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

    public void Hide() =>
        gameObject.SetActive(false);

    public void Show() =>
        gameObject.SetActive(false);
}