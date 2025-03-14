using UnityEngine;

public class CameraRotator : MonoBehaviour
{
    private const string MouseX = "Mouse X";
    private const string MouseY = "Mouse Y";

    [SerializeField] private CameraTarget _target;
    [SerializeField] private float _distance;
    [SerializeField] private float _sensitivity;
    [SerializeField] private float _verticalLimit;

    private float currentAngleX = 0f;
    private float currentAngleY = 0f;

    private void Awake()
    {
        currentAngleX = transform.eulerAngles.x;
        currentAngleY = transform.eulerAngles.y;
    }

    private void Update()
    {
        currentAngleY += Input.GetAxis(MouseX) * _sensitivity;
        currentAngleX -= Input.GetAxis(MouseY) * _sensitivity;
        currentAngleX = Mathf.Clamp(currentAngleX, -_verticalLimit, _verticalLimit);
    }

    private void LateUpdate()
    {
        Quaternion rotation = Quaternion.Euler(currentAngleX, currentAngleY, 0);
        Vector3 direction = new(0, 0, -_distance);

        transform.position = _target.transform.position + rotation * direction;
        transform.LookAt(_target.transform.position);
    }
}