using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(TriggerDetector))]
public abstract class Character : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private CharacterAnimationEvents _animationEvents;
    [SerializeField] private float _smoothValueMovement;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _moveSpeed;

    private TriggerDetector _triggerDetector;
    private Rigidbody _rigidbody;
    private Jumper _jumper;
    private Mover _mover;
    private CharacterAnimatorWrapping _animatorWrapping;
    private GroundDetector _groundDetector;
    private SpeedSmoother _smootherHorizontalAxis;
    private SpeedSmoother _smootherVerticalAxis;

    public event Action<TouchTrigger> Triggered;
    public event Action<SoundParams> SoundPlayBack;

    protected Animator GetAnimator => _animator;

    protected virtual void Awake()
    {
        _triggerDetector = GetComponent<TriggerDetector>();
        _rigidbody = GetComponent<Rigidbody>();

        _jumper = new(_rigidbody, _jumpForce);
        _mover = new(_rigidbody, _moveSpeed);
        _animatorWrapping = new(_animator);
        _groundDetector = new(transform, 0.14f);
        _groundDetector.Enable();
        _smootherHorizontalAxis = new(_smoothValueMovement);
        _smootherVerticalAxis = new(_smoothValueMovement);
    }

    private void OnEnable()
    {
        _triggerDetector.TouchTriggered += HandleTrigger;
        _animationEvents.LeftFoodSteppedOn += OnSoundLeftStep;
        _animationEvents.RightFoodSteppedOn += OnSoundRightStep;
        _groundDetector.DownJumped += OnSoundDownJump;
    }

    private void OnDisable()
    {
        _triggerDetector.TouchTriggered -= HandleTrigger;
        _animationEvents.LeftFoodSteppedOn -= OnSoundLeftStep;
        _animationEvents.RightFoodSteppedOn -= OnSoundRightStep;
        _groundDetector.DownJumped -= OnSoundDownJump;
    }

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
        if (_groundDetector.IsGrounded == false)
            return;

        _jumper.Jump();
        _animatorWrapping.ShowJump();
        PlaySound(SoundName.JumpUp);
    }

    public void Move(float valueRight, float valueForward)
    {
        valueRight = SmoothHorizontalAxis(valueRight);
        valueForward = SmoothVerticalAxis(valueForward);

        _animatorWrapping.ShowMove(valueRight, valueForward);
        _mover.Move(valueRight, valueForward);
    }

    protected float SmoothHorizontalAxis(float valueRight) =>
        _smootherHorizontalAxis.Smooth(valueRight);

    protected float SmoothVerticalAxis(float valueForward) =>
        _smootherVerticalAxis.Smooth(valueForward);

    private void HandleTrigger(TouchTrigger trigger) =>
        Triggered?.Invoke(trigger);

    private void OnSoundDownJump() =>
        PlaySound(SoundName.JumpDown);

    private void OnSoundLeftStep()
    {
        SoundName name = SoundName.LeftStepGrass;

        switch (_groundDetector.GroundMarker)
        {
            case GrassMarker:
                name = SoundName.LeftStepGrass;
                break;

            case WoodFloor:
                name = SoundName.LeftStepWoodFloor;
                break;
        }

        PlaySound(name);
    }

    private void OnSoundRightStep()
    {
        SoundName name = SoundName.LeftStepGrass;

        switch (_groundDetector.GroundMarker)
        {
            case GrassMarker:
                name = SoundName.RightStepGrass;
                break;

            case WoodFloor:
                name = SoundName.RightStepWoodFloor;
                break;
        }

        PlaySound(name);
    }

    protected void PlaySound(SoundName name) =>
        SoundPlayBack?.Invoke(new(name, transform));
}