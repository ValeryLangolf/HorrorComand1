using System;
using UnityEngine;

public class TriggerDetector : ColliderObject
{
    public event Action<TouchTrigger> TouchTriggered;
    public event Action<TeleportTrigger> TeleportTriggered;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out TouchTrigger trigger))
            TouchTriggered?.Invoke(trigger);

        if (other.TryGetComponent(out TeleportTrigger teleportMarker))
            TeleportTriggered?.Invoke(teleportMarker);
    }
}