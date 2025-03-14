using System;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private CameraVerticalRotator _cameraVerticalRotator;
    [SerializeField] private float _runSpeedMultiplier;
    [SerializeField] private bool _isReadInputs;

    private readonly PlayerInput _input = new();

    private bool _isShowBook;

    public event Action InteractionButtonPressed;

    private void Awake()
    {
        _cameraVerticalRotator.EnableRotate();
    }

    private void Update()
    {
        //if (_isShowBook == false)
        //    _player.Rotate();

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
        _cameraVerticalRotator.DisableRotate();
    }

    private void HideBook()
    {
        _player.HideBook();
        _cameraVerticalRotator.EnableRotate();
    }
}