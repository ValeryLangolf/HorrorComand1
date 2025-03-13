using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(PlayerBody))]
[RequireComponent(typeof(TriggerDetector))]
public class Player : Character
{
    [SerializeField] private Animator _animator;
    [SerializeField] private PlayerAnimationEvents _animationEvents;
    [SerializeField] private PlayerHead _head;
    [SerializeField] private GroundDetector _groundDetector;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private bool _isReadInputs;

    [SerializeField] private AudioClip _leftStep;
    [SerializeField] private AudioClip _rightStep;

    private Rigidbody _rigidbody;
    private PlayerBody _body;

    private readonly PlayerInput _input = new();
    private TriggerDetector _triggerDetector;
    private PlayerAnimator _playerAnimator;
    private PlayerBodyRotator _bodyRotator;
    private PlayerHeadRotator _headRotator;
    private Jumper _jumper;
    private Mover _mover;
    private SpeedSmoother _smootherHorizontalAxis;
    private SpeedSmoother _smootherVerticalAxis;

    public event Action InteractionButtonPressed;
    public event Action<TouchTrigger> Triggered;

    protected override void Awake()
    {
        base.Awake();

        _rigidbody = GetComponent<Rigidbody>();
        _body = GetComponent<PlayerBody>();
        _triggerDetector = GetComponent<TriggerDetector>();

        _playerAnimator = new(this, _animator);
        _bodyRotator = new(_body);
        _headRotator = new(_body, _head);
        _jumper = new(_rigidbody, _jumpForce);
        _mover = new(transform, _rigidbody, _moveSpeed);
        _smootherHorizontalAxis = new(4f);
        _smootherVerticalAxis = new(4f);
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
        _animationEvents.RightFoodSteppedOn += OnSoundRightStep;
    }

    private void FixedUpdate()
    {
        float right = 0;
        float forward = 0;

        if (_isReadInputs)
            _input.IsMovePressed(out right, out forward);

        OnMove(right, forward);

        _headRotator.RotateHead();

        if (_isReadInputs == false)
            return;

        if (_input.IsJumpPressed())
            OnJump();

        if (_input.IsInteractionButtonPressed())
            InteractionButtonPressed?.Invoke();
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
        _playerAnimator.ShowGettingUp(callback);

    public void ShowSitting(AnimationFinishedCallback callback = null) =>
        _playerAnimator.ShowSitting(callback);

    public void ShowSitToStand(AnimationFinishedCallback callback = null) =>
        _playerAnimator.ShowSitToStand(callback);

    private void HandleTrigger(TouchTrigger trigger) =>
        Triggered?.Invoke(trigger);

    private void OnJump()
    {
        if (_groundDetector.IsGround() == false)
            return;

        _jumper.Jump();
        _playerAnimator.ShowJump();
    }

    private void OnMove(float valueRight, float valueForward)
    {
        if (_input.IsShift() && valueForward > 0)
            valueForward *= 4;

        valueRight = _smootherHorizontalAxis.Smooth(valueRight);
        valueForward = _smootherVerticalAxis.Smooth(valueForward);

        _playerAnimator.ShowMove(valueRight, valueForward);

        if (valueRight == 0 && valueForward == 0)
            return;

        _mover.Move(valueRight, valueForward);
        _bodyRotator.Rotate();
    }

    private void OnSoundLeftStep() =>
        PlaySound(_leftStep);

    private void OnSoundRightStep() =>
        PlaySound(_rightStep);
}