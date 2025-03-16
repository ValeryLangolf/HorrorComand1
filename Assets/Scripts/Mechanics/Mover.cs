using UnityEngine;

public class Mover
{
    private const float ForwardSpeedMultiplier = 0.5f;
    private const float BackSpeedMultiplier = 0.4f;
    private const float HorizontalSpeedMultiplier = 0.4f;

    private readonly Transform _transform;
    private readonly Rigidbody _rigidbody;
    private readonly float _speed;

    public Mover(Rigidbody rigidbody, float speed)
    {
        _transform = rigidbody.GetComponent<Transform>();
        _rigidbody = rigidbody;
        _speed = speed;
    }

    public void Move(float valueRight, float valueForward)
    {
        if (valueRight == 0 && valueForward == 0)
            return;

        Vector3 worldDirection = _transform.TransformDirection(new(
            NormalizeHorizontalSpeed(valueRight),
            0,
            NormalizeVerticalSpeed(valueForward)));

        _rigidbody.MovePosition(_rigidbody.position + _speed * Time.deltaTime * worldDirection);
    }

    private float NormalizeHorizontalSpeed(float speed) =>
        speed * HorizontalSpeedMultiplier;        

    private float NormalizeVerticalSpeed(float speed) =>
        speed < 0 ? speed * BackSpeedMultiplier : speed * ForwardSpeedMultiplier;
}