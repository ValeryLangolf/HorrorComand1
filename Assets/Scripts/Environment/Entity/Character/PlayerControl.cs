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
    private CallbackFinished _currentCallback;

    public bool IsBookShowed { get; private set; }

    public bool IsCanReadInput { get; private set; }

    public event Action ShowingBookChanged;
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
        EnableInputReader();
        IsCanReadInput = true;
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

    public void EnableRotate()
    {
        _verticalRotator.Enable();
        _horizontalRotator.Enable();
    }

    public void DisableRotate()
    {
        _verticalRotator.Disable();
        _horizontalRotator.Disable();

        _player.Move(0, 0, false, false, false);
    }

    public void EnableInputReader() =>
        _inputReader.Enable();

    public void DisableInputReader() =>
        _inputReader.Disable();

    public void TakeNote(NoteInWorldTrigger note)
    {
        _book.TakeNote(note);
        ShowBook(_book.ZoomInCurrentNote);
    }

    private void ShowBook(CallbackFinished callback = null)
    {
        IsBookShowed = true;
        IsCanReadInput = false;

        ShowingBookChanged?.Invoke();

        _inputReader.Disable();
        _cameraLever.DisableStabilize();

        _currentCallback = callback;

        _book.ShowObject();
        _player.ShowBook(HandleAfterShowBook);
        _cameraPositionAdjuster.Adjust(_cameraLever.transform, _cameraTargetShowBook.transform);
    }

    private void HandleAfterShowBook()
    {
        IsCanReadInput = true;
        _inputReader.Enable();
        _currentCallback?.Invoke();
    }

    private void HideBook()
    {
        _player.HideBook(HandleAfterHideBook);
        _cameraPositionAdjuster.Adjust(_cameraLever.transform, _cameraTargetHideBook.transform, _cameraLever.EnableStabilize);
    }

    private void HandleAfterHideBook()
    {
        IsBookShowed = false;
        IsCanReadInput = true;

        ShowingBookChanged?.Invoke();

        _inputReader.Enable();
        _book.HideObject();
    }

    private void OnJump()
    {
        if (IsBookShowed)
            return;

        _player.OnJump();
    }

    private void OnMove(float horizontalValue, float verticalValue)
    {
        if (IsBookShowed)
            return;

        _player.Move(horizontalValue, verticalValue, _inputReader.IsSitting, _inputReader.IsShift, true);
    }

    private void HandleBookKeyPressed()
    {
        if (IsBookShowed)
        {
            IsCanReadInput = false;
            _inputReader.Disable();
            _cameraLever.DisableStabilize();

            _book.ZoomOutNote(HideBook);
        }
        else
            ShowBook();
    }

    private void InvokeInteractionKeyPressed()
    {
        if (IsBookShowed)
            return;

        InteractionKeyPressed?.Invoke();
    }
}