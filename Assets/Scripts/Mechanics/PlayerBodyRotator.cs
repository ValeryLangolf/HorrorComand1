using UnityEngine;

public class PlayerBodyRotator
{
    private const float Speed = 4f;

    private readonly Transform _transform;
    private readonly Transform _camera;

    public PlayerBodyRotator(PlayerBody playerBody)
    {
        _transform = playerBody.transform;
        _camera = Camera.main.transform;
    }

    public void Rotate()
    {
        Vector3 cameraForward = new Vector3(_camera.forward.x, 0, _camera.forward.z).normalized;

        if (cameraForward == Vector3.zero)
            return;

        Quaternion target = Quaternion.LookRotation(cameraForward);
        _transform.rotation = Quaternion.Slerp(_transform.rotation, target, Time.deltaTime * Speed);
    }
}