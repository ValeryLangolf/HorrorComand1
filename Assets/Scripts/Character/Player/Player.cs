using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(TriggerDetector))]
public class Player : Character
{
    private const float SmoothValueMovement = 4f;

    [SerializeField] private Animator _animator;
    [SerializeField] private PlayerAnimationEvents _animationEvents;
    [SerializeField] private GroundDetector _groundDetector;

    [SerializeField] private float _jumpForce;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _runSpeedMultiplier;
    [SerializeField] private bool _isReadInputs;

    [SerializeField] private AudioClip _leftStep;
    [SerializeField] private AudioClip _rightStep;

    private Rigidbody _rigidbody;

    private readonly PlayerInput _input = new();
    private TriggerDetector _triggerDetector;
    private PlayerAnimatorWrapping _animatorWrapping;
    private PlayerRotator _bodyRotator;
    private Jumper _jumper;
    private Mover _mover;
    private SpeedSmoother _smootherHorizontalAxis;
    private SpeedSmoother _smootherVerticalAxis;

    private bool _isShowBook;

    public event Action InteractionButtonPressed;
    public event Action<TouchTrigger> Triggered;

    protected override void Awake()
    {
        base.Awake();

        _rigidbody = GetComponent<Rigidbody>();
        _triggerDetector = GetComponent<TriggerDetector>();

        _animatorWrapping = new(this, _animator);
        _bodyRotator = new(transform);
        _jumper = new(_rigidbody, _jumpForce);
        _mover = new(transform, _rigidbody, _moveSpeed);
        _smootherHorizontalAxis = new(SmoothValueMovement);
        _smootherVerticalAxis = new(SmoothValueMovement);
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

    private void Update()
    {
        if (_isShowBook == false)
            _bodyRotator.Rotate();

        float right = 0;
        float forward = 0;

        if (_isReadInputs)
            _input.IsMovePressed(out right, out forward);

        OnMove(right, forward);

        if (_isReadInputs == false)
            return;

        if (_input.IsJumpPressed())
            OnJump();

        if (_input.IsInteractionButtonPressed())
            InteractionButtonPressed?.Invoke();

        if (_input.IsBookPressed())
            HandleBookPressed();
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

    public void EnableControl() =>
        _isReadInputs = true;

    public void DisableControl() =>
        _isReadInputs = false;

    public void ShowGettingUp(AnimationFinishedCallback callback = null) =>
        _animatorWrapping.ShowGettingUp(callback);

    public void ShowSitting(AnimationFinishedCallback callback = null) =>
        _animatorWrapping.ShowSitting(callback);

    public void ShowSitToStand(AnimationFinishedCallback callback = null) =>
        _animatorWrapping.ShowSitToStand(callback);

    private void HandleTrigger(TouchTrigger trigger) =>
        Triggered?.Invoke(trigger);

    private void OnJump()
    {
        if (_groundDetector.IsGround() == false)
            return;

        _jumper.Jump();
        _animatorWrapping.ShowJump();
    }

    private void HandleBookPressed()
    {
        _isShowBook = !_isShowBook;

        if (_isShowBook)
            _animatorWrapping.ShowBook();
        else
            _animatorWrapping.HideBook();
    }

    private void OnMove(float valueRight, float valueForward)
    {
        if (_input.IsShift() && valueForward > 0)
            valueForward *= _runSpeedMultiplier;

        valueRight = _smootherHorizontalAxis.Smooth(valueRight);
        valueForward = _smootherVerticalAxis.Smooth(valueForward);

        _animatorWrapping.ShowMove(valueRight, valueForward);

        if (valueRight != 0 || valueForward != 0)
            _mover.Move(valueRight, valueForward);
    }

    private void OnSoundLeftStep() =>
        PlaySound(_leftStep);

    private void OnSoundRightStep() =>
        PlaySound(_rightStep);
}