using System;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class TriggerDetector : MonoBehaviour
{
    private Collider _collider;

    public event Action<TouchTrigger> TouchTriggered;
    public event Action<TeleportMarker> TeleportTriggered;

    private void Awake() =>
        _collider = GetComponent<Collider>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out TouchTrigger trigger))
            TouchTriggered?.Invoke(trigger);

        if (other.TryGetComponent(out TeleportMarker teleportMarker))
            TeleportTriggered?.Invoke(teleportMarker);
    }

    public void EnableCollider() =>
        _collider.enabled = true;

    public void DisableCollider() =>
        _collider.enabled = false;
}