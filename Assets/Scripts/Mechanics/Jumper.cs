using UnityEngine;

public class Jumper
{
    private readonly Rigidbody _rigidbody;
    private readonly float _force;
    private readonly Vector3 _direction = Vector3.up;

    public Jumper(Rigidbody rigidbody, float force)
    {
        _rigidbody = rigidbody;
        _force = force;
    }

    public void Jump()
    {
        Vector3 velocity = _rigidbody.velocity;
        velocity.y = 0;

        _rigidbody.velocity = velocity;
        _rigidbody.AddForce(_direction * _force, ForceMode.VelocityChange);
    }
}