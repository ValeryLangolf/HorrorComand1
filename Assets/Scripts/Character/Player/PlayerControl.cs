using System;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private CameraLever _cameraLever;
    [SerializeField] private float _runSpeedMultiplier;
    [SerializeField] private float _mouseSensitivity;
    [SerializeField] private bool _isReadInputs;

    private readonly PlayerInput _input = new();
    private CameraVerticalRotator _cameraVerticalRotator;
    private PlayerBodyHorizontalRotator _playerBodyHorizontalRotator;

    private bool _isShowBook;

    public event Action InteractionButtonPressed;

    private void Awake()
    {
        _cameraVerticalRotator = new(_cameraLever.transform, _mouseSensitivity);
        _playerBodyHorizontalRotator = new(transform, _mouseSensitivity);
    }

    private void Update()
    {
        if (_isShowBook == false)
        {
            _cameraVerticalRotator.Rotate();
            _playerBodyHorizontalRotator.Rotate();
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

        if (_input.IsJumpPressed())
            _player.Jump();

        if (_input.IsInteractionButtonPressed())
            InteractionButtonPressed?.Invoke();

        if (_input.IsBookPressed())
        {
            _isShowBook = !_isShowBook;

            if (_isShowBook)
                ShowBook();
            else
                HideBook();
        }
    }

    public void EnableControl() =>
        _isReadInputs = true;

    public void DisableControl() =>
        _isReadInputs = false;

    private void ShowBook()
    {
        _player.ShowBook();
        _isShowBook = true;
    }

    private void HideBook()
    {
        _player.HideBook();
        _isShowBook = false;
    }
}