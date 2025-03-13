using UnityEngine;
public class PlayerHeadRotator
{
    private const float HorizontalHeadLimit = 80f;
    private const float SpeedHead = 6f;

    private readonly Transform _camera;
    private readonly Transform _bodyTransform;
    private readonly Transform _headTransform;

    public PlayerHeadRotator(PlayerBody body, PlayerHead head)
    {
        _camera = Camera.main.transform;
        _bodyTransform = body.transform;
        _headTransform = head.transform;
    }

    public void RotateHead()
    {
        float angleY = NormalizeAngle(_camera.eulerAngles.y - _bodyTransform.eulerAngles.y);

        angleY = Mathf.Clamp(angleY, - HorizontalHeadLimit, HorizontalHeadLimit);
        angleY += _bodyTransform.eulerAngles.y;

        float angleX = NormalizeAngle(_camera.eulerAngles.x);

        Quaternion targetRotation = Quaternion.Euler(angleX, angleY, 0);
        _headTransform.rotation = Quaternion.Slerp(_headTransform.rotation, targetRotation, Time.deltaTime * SpeedHead);
    }

    private float NormalizeAngle(float angle)
    {
        angle %= 360;

        if (angle < -180)
            angle += 360;

        else if (angle > 180)
            angle -= 360;

        return angle;
    }
}