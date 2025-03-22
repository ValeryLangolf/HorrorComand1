using System;
using UnityEngine;

public class TriggerDetector : ObjectCollider
{
    public event Action<Collider> Triggered;

    private void OnTriggerEnter(Collider other) =>
        Triggered?.Invoke(other);
}