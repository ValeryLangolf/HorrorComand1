using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Teleportator : MonoBehaviour
{
    private const float Height = 10f;

    [SerializeField] private AnomalyTerrain _terrain;
    [SerializeField] private TriggerDetector _triggerDetector;

    private Rigidbody _rigidbody;

    private void Awake() =>
        _rigidbody = GetComponent<Rigidbody>();

    private void OnEnable() =>
        _triggerDetector.Triggered += HandleTeleportation;

    private void OnDisable() =>
        _triggerDetector.Triggered -= HandleTeleportation;

    private void HandleTeleportation(Collider other)
    {
        if (other.TryGetComponent(out TeleportTrigger teleportMarker) == false)
            return;

        switch (teleportMarker)
        {
            case LeftTeleport _:
                TeleportSide(new(_terrain.Width, 0, 0));
                break;

            case RightTeleport _:
                TeleportSide(new(-_terrain.Width, 0, 0));
                break;

            case UpTeleport _:
                TeleportSide(new(0, 0, -_terrain.Height));
                break;

            case DownTeleport _:
                TeleportSide(new(0, 0, _terrain.Height));
                break;

            case UndergroundTeleport _:
                TeleportUp(new(0, Height, 0));
                break;

            default:
                throw new ArgumentOutOfRangeException("Unknown");
        }
    }

    private void TeleportUp(Vector3 distance)
    {
        TeleportSide(distance);
        _rigidbody.velocity = Vector3.zero;
    }

    private void TeleportSide(Vector3 distance) =>
        transform.position += distance;
}