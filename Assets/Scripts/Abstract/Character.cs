using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(TriggerDetector))]
public abstract class Character : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private CharacterAnimationEvents _animationEvents;
    [SerializeField] private GroundDetector _groundDetector;
    [SerializeField] private float _smoothValueMovement;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _moveSpeed;

    [SerializeField] private AudioClip _leftStep;
    [SerializeField] private AudioClip _rightStep;

    private CharacterAnimatorWrapping _animatorWrapping;
    private SoundEffectPlayer _soundPlayer;
    private TriggerDetector _triggerDetector;
    private Rigidbody _rigidbody;
    private CharacterRotator _bodyRotator;
    private Jumper _jumper;
    private Mover _mover;
    private SpeedSmoother _smootherHorizontalAxis;
    private SpeedSmoother _smootherVerticalAxis;

    public event Action<TouchTrigger> Triggered;    

    protected Animator Animator => _animator;

    public bool IsGrounded => _groundDetector.IsGrounded();

    protected virtual void Awake()
    {
        _triggerDetector = GetComponent<TriggerDetector>();
        _rigidbody = GetComponent<Rigidbody>();

        _soundPlayer = new(this);
        _bodyRotator = new(transform);
        _jumper = new(_rigidbody, _jumpForce);
        _mover = new(transform, _rigidbody, _moveSpeed);
        _animatorWrapping = new(this, _animator);
        _smootherHorizontalAxis = new(_smoothValueMovement);
        _smootherVerticalAxis = new(_smoothValueMovement);
    }

    private void OnEnable()
    {
        _triggerDetector.TouchTriggered += HandleTrigger;
        _animationEvents.LeftFoodSteppedOn += OnSoundLeftStep;
        _animationEvents.RightFoodSteppedOn += OnSoundRightStep;
    }

    private void OnDisable()
    {
        _triggerDetector.TouchTriggered -= HandleTrigger;
        _animationEvents.LeftFoodSteppedOn -= OnSoundLeftStep;
        _animationEvents.RightFoodSteppedOn -= OnSoundRightStep;
    }

    public void PlaySound(AudioClip clip, AudioFinishedCallback callback = null) =>
        _soundPlayer.Play(clip, callback);

    public void EnableCollider() =>
        _triggerDetector.EnableCollider();

    public void DisableCollider() =>
        _triggerDetector.DisableCollider();

    public void EnableGravity() =>
        _rigidbody.useGravity = true;

    public void DisableGravity()
    {
        _rigidbody.useGravity = false;
        _rigidbody.velocity = Vector3.zero;
    }

    public void Jump()
    {
        if (IsGrounded == false)
            return;

        _jumper.Jump();
        _animatorWrapping.ShowJump();
    }

    public void Move(float valueRight, float valueForward)
    {
        valueRight = SmoothHorizontalAxis(valueRight);
        valueForward = SmoothVerticalAxis(valueForward);

        _animatorWrapping.ShowMove(valueRight, valueForward);
        _mover.Move(valueRight, valueForward);
    }

    public void Rotate() =>
        _bodyRotator.Rotate();

    protected float SmoothHorizontalAxis(float valueRight) =>
        _smootherHorizontalAxis.Smooth(valueRight);

    protected float SmoothVerticalAxis(float valueForward) =>
        _smootherVerticalAxis.Smooth(valueForward);

    private void HandleTrigger(TouchTrigger trigger) =>
        Triggered?.Invoke(trigger);

    private void OnSoundLeftStep() =>
        PlaySound(_leftStep);

    private void OnSoundRightStep() =>
        PlaySound(_rightStep);
}