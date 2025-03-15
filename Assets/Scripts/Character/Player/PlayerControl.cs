using System;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private const float SpeedMovingCameraAdjuster = 1f;
    private const float SpeedRotationCameraAdjuster = 150f;

    [SerializeField] private Player _player;
    [SerializeField] private CameraLever _cameraLever;
    [SerializeField] private CameraTarget _cameraTargetShowBook;
    [SerializeField] private CameraTarget _cameraTargetHideBook;
    [SerializeField] private float _runSpeedMultiplier;
    [SerializeField] private float _mouseSensitivity;
    [SerializeField] private bool _isReadInputs;

    private readonly PlayerInput _input = new();
    private CameraVerticalRotator _cameraVerticalRotator;
    private PlayerBodyHorizontalRotator _playerBodyHorizontalRotator;
    private PositionAdjuster _cameraPositionAdjuster;

    private bool _isShowBook;

    public event Action InteractionButtonPressed;

    private void Awake()
    {
        _cameraVerticalRotator = new(_cameraLever.transform, _mouseSensitivity);
        _playerBodyHorizontalRotator = new(transform, _mouseSensitivity);
        _cameraPositionAdjuster = new(this, SpeedMovingCameraAdjuster, SpeedRotationCameraAdjuster);
    }

    private void Start()
    {
        HideCursor();
    }

    private void Update()
    {
        if (_isShowBook == false)
        {
            _cameraVerticalRotator.Rotate();
            _playerBodyHorizontalRotator.Rotate();

            if (_input.IsJumpPressed())
                _player.Jump();
        }

        float right = 0;
        float forward = 0;

        if (_isReadInputs)
            _input.IsMovePressed(out right, out forward);

        if (_input.IsShift() && forward > 0)
            forward *= _runSpeedMultiplier;

        _player.Move(right, forward);

        if (_isReadInputs == false)
            return;


        if (_input.IsInteractionButtonPressed())
            InteractionButtonPressed?.Invoke();

        if (_input.IsBookPressed())
        {
            if (_isShowBook)
                HideBook();
            else
                ShowBook();
        }
    }

    public void EnableControl() =>
        _isReadInputs = true;

    public void DisableControl() =>
        _isReadInputs = false;

    private void ShowBook()
    {
        _player.ShowBook();
        ShowCursor();
        _cameraPositionAdjuster.Adjust(_cameraLever.transform, _cameraTargetShowBook.transform);
        _isShowBook = true;
    }

    private void HideBook()
    {
        _player.HideBook();
        HideCursor();
        _cameraPositionAdjuster.Adjust(_cameraLever.transform, _cameraTargetHideBook.transform, ReturnCameraControl);
    }

    private void ReturnCameraControl()
    {
        _cameraVerticalRotator.SetCurrentRotation();
        _player.DeactivateBook();
        _isShowBook = false;
    }

    private void ShowCursor() =>
        Cursor.visible = true;

    private void HideCursor() =>
        Cursor.visible = false;

    private void OnApplicationFocus(bool focus)
    {
        if (focus && _isShowBook == false)
            HideCursor();
        else
            ShowCursor();
    }
}