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

    private void HandleTeleportation(TeleportMarker teleportMarker)
    {
        Vector3 amendment = Vector3.zero;

        if (teleportMarker is UndergroundTeleport)
        {
            amendment = new(0, Height, 0);
            _rigidbody.velocity = Vector3.zero;
        }

        if (teleportMarker is LeftTeleport)
            amendment = new(_terrain.Width, 0, 0);

        if (teleportMarker is RightTeleport)
            amendment = new(-_terrain.Width, 0, 0);

        if (teleportMarker is UpTeleport)
            amendment = new(0, 0, -_terrain.Height);

        if (teleportMarker is DownTeleport)
            amendment = new(0, 0, _terrain.Height);

        transform.position += amendment;
    }
}