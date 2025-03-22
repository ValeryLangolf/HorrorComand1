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

    public void OnJump()
    {
        _rigidbody.velocity = new Vector3(_rigidbody.velocity.x, 0, _rigidbody.velocity.z);
        _rigidbody.AddForce(_direction * _force, ForceMode.VelocityChange);
    }
}