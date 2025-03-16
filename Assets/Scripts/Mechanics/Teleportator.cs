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
        _triggerDetector.TeleportTriggered += HandleTeleportation;

    private void OnDisable() =>
        _triggerDetector.TeleportTriggered -= HandleTeleportation;

    private void HandleTeleportation(TeleportTrigger teleportMarker)
    {
        switch (teleportMarker)
        {
            case LeftTeleport _:
                MoveSide(new(_terrain.Width, 0, 0));
                break;

            case RightTeleport _:
                MoveSide(new(-_terrain.Width, 0, 0));
                break;

            case UpTeleport _:
                MoveSide(new(0, 0, -_terrain.Height));
                break;

            case DownTeleport _:
                MoveSide(new(0, 0, _terrain.Height));
                break;

            case UndergroundTeleport _:
                MoveUp(new(0, Height, 0));
                break;

            default:
                throw new ArgumentOutOfRangeException("unknown");
        }
    }

    private void MoveUp(Vector3 distance)
    {
        MoveSide(distance);
        _rigidbody.velocity = Vector3.zero;
    }

    private void MoveSide(Vector3 distance) =>
        transform.position += distance;
}