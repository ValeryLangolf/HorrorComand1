using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(TriggerDetector))]
public abstract class Character : Entity
{
    [SerializeField] private Animator _animator;
    [SerializeField] private CharacterAnimationEvents _animationEvents;
    [SerializeField] private GroundDetector _groundDetector;
    [SerializeField] private float _smoothValueMovement;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _runningSpeedMultiplier;

    private TriggerDetector _triggerDetector;
    private Rigidbody _rigidbody;
    private Jumper _jumper;
    private Mover _mover;
    private CharacterAnimatorWrapping _animatorWrapping;
    private ValueSmoother _smootherHorizontalAxis;
    private ValueSmoother _smootherVerticalAxis;

    public event Action<TouchTrigger> Touched;
    public event Action<SoundParams> SoundPlayBack;

    protected Animator GetAnimator => _animator;

    protected virtual void Awake()
    {
        _triggerDetector = GetComponent<TriggerDetector>();
        _rigidbody = GetComponent<Rigidbody>();

        _jumper = new(_rigidbody, _jumpForce);
        _mover = new(_rigidbody, _moveSpeed);
        _animatorWrapping = new(_animator);
        _smootherHorizontalAxis = new(_smoothValueMovement);
        _smootherVerticalAxis = new(_smoothValueMovement);
    }

    private void OnEnable()
    {
        _triggerDetector.Triggered += HandleTrigger;
        _animationEvents.LeftFoodSteppedOn += PlaySoundLeftStep;
        _animationEvents.RightFoodSteppedOn += PlaySoundRightStep;
        _groundDetector.Landed += OnSoundDownJump;
    }

    private void OnDisable()
    {
        _triggerDetector.Triggered -= HandleTrigger;
        _animationEvents.LeftFoodSteppedOn -= PlaySoundLeftStep;
        _animationEvents.RightFoodSteppedOn -= PlaySoundRightStep;
        _groundDetector.Landed -= OnSoundDownJump;
    }

    public void EnableGravity() =>
        _rigidbody.useGravity = true;

    public void DisableGravity()
    {
        _rigidbody.useGravity = false;
        _rigidbody.velocity = Vector3.zero;
    }

    public void OnJump()
    {
        if (_groundDetector.IsGrounded() == false)
            return;

        _jumper.OnJump();
        _animatorWrapping.ShowJump();
        PlaySound(SoundName.JumpUp);
    }

    public void Move(float valueRight, float valueForward, bool isSitting, bool isShift, bool isSmooth)
    {
        if (isShift && isSitting == false && valueForward > 0)
            valueForward *= _runningSpeedMultiplier;

        if (isSmooth)
        {
            valueRight = _smootherHorizontalAxis.Smooth(valueRight);
            valueForward = _smootherVerticalAxis.Smooth(valueForward);
        }        

        if (isSitting)
            _animatorWrapping.ShowSittingMove(valueRight, valueForward);
        else
            _animatorWrapping.ShowMove(valueRight, valueForward);

        _mover.OnMove(valueRight, valueForward);
    }

    private void HandleTrigger(Collider other)
    {
        if (other.TryGetComponent(out TouchTrigger touchTrigger))
            Touched?.Invoke(touchTrigger);
    }

    private void OnSoundDownJump() =>
        PlaySound(SoundName.JumpDown);

    private void PlaySoundLeftStep()
    {
        switch (_groundDetector.GetGroundMarker())
        {
            case GrassMarker:
                PlaySound(SoundName.LeftStepGrass);
                break;

            case WoodFloor:
                PlaySound(SoundName.LeftStepWoodFloor);
                break;

            default:
                PlaySound(SoundName.LeftStepGrass);
                break;
        }
    }

    private void PlaySoundRightStep()
    {
        switch (_groundDetector.GetGroundMarker())
        {
            case GrassMarker:
                PlaySound(SoundName.RightStepGrass);
                break;

            case WoodFloor:
                PlaySound(SoundName.RightStepWoodFloor);
                break;

            default:
                PlaySound(SoundName.RightStepGrass);
                break;
        }
    }

    protected void PlaySound(SoundName name) =>
        SoundPlayBack?.Invoke(new(name, transform));
}