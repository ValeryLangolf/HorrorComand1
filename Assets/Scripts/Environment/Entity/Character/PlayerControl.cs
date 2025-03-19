using System;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private const float SpeedMovingCameraAdjuster = 1f;
    private const float SpeedRotationCameraAdjuster = 150f;

    [SerializeField] private Player _player;
    [SerializeField] private Book _book;
    [SerializeField] private CameraLever _cameraLever;
    [SerializeField] private CameraTarget _cameraTargetShowBook;
    [SerializeField] private CameraTarget _cameraTargetHideBook;    
    [SerializeField] private float _mouseSensitivity;

    private InputReader _inputReader;
    private VerticalRotator _verticalRotator;
    private HorizontalRotator _horizontalRotator;
    private PositionAdjuster _cameraPositionAdjuster;

    private bool _isBookShowed;

    public event Action InteractionKeyPressed;

    private void Awake()
    {
        _verticalRotator = new(_cameraLever.transform, _mouseSensitivity);
        _horizontalRotator = new(transform, _mouseSensitivity);
        _cameraPositionAdjuster = new(this, SpeedMovingCameraAdjuster, SpeedRotationCameraAdjuster);
        _inputReader = new(this);
    }

    private void Start()
    {
        HideCursor();
        EnableControl();
    }

    private void OnEnable()
    {
        _inputReader.JumpPressed += OnJump;
        _inputReader.MovePressed += OnMove;
        _inputReader.InteractionKeyPressed += InvokeInteractionKeyPressed;
        _inputReader.BookKeyPressed += HandleBookKeyPressed;
    }

    private void OnDisable()
    {
        _inputReader.JumpPressed -= OnJump;
        _inputReader.MovePressed -= OnMove;
        _inputReader.InteractionKeyPressed -= InvokeInteractionKeyPressed;
        _inputReader.BookKeyPressed -= HandleBookKeyPressed;
    }

    public void EnableControl()
    {
        _inputReader.Enable();
        _verticalRotator.Enable();
        _horizontalRotator.Enable();
    }

    public void DisableControl()
    {
        _inputReader.Disable();
        _verticalRotator.Disable();
        _horizontalRotator.Disable();
    }

    public void ShowBook(AnimationFinishedCallback callback = null)
    {
        _player.ShowBook(callback);
        ShowCursor();
        _verticalRotator.Disable();
        _horizontalRotator.Disable();
        _cameraPositionAdjuster.Adjust(_cameraLever.transform, _cameraTargetShowBook.transform);
        _isBookShowed = true;
    }

    private void HideBook()
    {
        _player.HideBook();
        HideCursor();
        _cameraPositionAdjuster.Adjust(_cameraLever.transform, _cameraTargetHideBook.transform, ReturnCameraControl);
    }

    private void OnJump()
    {
        if (_isBookShowed)
            return;

        _player.Jump();
    }

    private void OnMove(float horizontalValue, float verticalValue)
    {
        if (_isBookShowed)
            return;

        _player.Move(horizontalValue, verticalValue, _inputReader.IsSitting, _inputReader.IsShift);
    }

    private void InvokeInteractionKeyPressed() =>
        InteractionKeyPressed?.Invoke();

    private void HandleBookKeyPressed()
    {
        if (_isBookShowed)
            _book.ZoomOutNote(HideBook);
        else
            ShowBook();
    }

    private void ReturnCameraControl()
    {
        _verticalRotator.Enable();
        _horizontalRotator.Enable();
        _player.DeactivateBook();
        _isBookShowed = false;
    }

    private void ShowCursor() =>
        Cursor.visible = true;

    private void HideCursor() =>
        Cursor.visible = false;

    private void OnApplicationFocus(bool focus)
    {
        if (focus && _isBookShowed == false)
            HideCursor();
        else
            ShowCursor();
    }
}