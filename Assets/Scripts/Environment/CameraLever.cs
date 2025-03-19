using UnityEngine;

public class CameraLever : MonoBehaviour 
{
    [SerializeField] private CameraTarget _cameraFollowTarget;
    [SerializeField] private float _speed;

    private PositionStabilizator _cameraStabilizator;

    private void Awake()
    {
        _cameraStabilizator = new(transform, _cameraFollowTarget.transform, _speed);
        Enable();
    }

    public void Enable() =>
        _cameraStabilizator.Enable();

    public void Disable() =>
        _cameraStabilizator.Disable();
}