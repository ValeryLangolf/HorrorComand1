using System;
using System.Collections;
using UnityEngine;

public class InputReader
{
    private const string Horizontal = "Horizontal";
    private const string Vertical = "Vertical";

    private const KeyCode _jump = KeyCode.Space;
    private const KeyCode _shift = KeyCode.LeftShift;
    private const KeyCode _interaction = KeyCode.E;
    private const KeyCode _book = KeyCode.Tab;
    private const KeyCode _sit = KeyCode.LeftControl;

    private readonly MonoBehaviour _monoBehaviour;
    private Coroutine _coroutine;

    public event Action JumpPressed;
    public event Action InteractionKeyPressed;
    public event Action BookKeyPressed;
    public event Action<float, float> MovePressed;

    public InputReader(MonoBehaviour monoBehaviour)
    {
        _monoBehaviour = monoBehaviour;
    }

    public bool IsShift => 
        Input.GetKey(_shift);

    public bool IsSitting =>
        Input.GetKey(_sit);

    public void Enable()
    {
        Disable();
        _coroutine = _monoBehaviour.StartCoroutine(ReadInputOverTime());
    }

    public void Disable()
    {
        if (_coroutine != null)
            _monoBehaviour.StopCoroutine(_coroutine);
    }

    private IEnumerator ReadInputOverTime()
    {
        while (true)
        {
            ReadInputs();

            yield return null;
        }
    }

    private void ReadInputs()
    {
        ReadJump();
        ReadKeyInteraction();
        ReadKeyBook();
        ReadMoveInput();
    }

    private void ReadJump()
    {
        if (Input.GetKeyDown(_jump))
            JumpPressed?.Invoke();
    }

    private void ReadKeyInteraction()
    {
        if (Input.GetKeyDown(_interaction))
            InteractionKeyPressed?.Invoke();
    }

    private void ReadKeyBook()
    {
        if (Input.GetKeyDown(_book))
            BookKeyPressed?.Invoke();
    }

    private void ReadMoveInput()
    {
        float horizontalAxis = Input.GetAxis(Horizontal);
        float verticalAxis = Input.GetAxis(Vertical);

        MovePressed?.Invoke(horizontalAxis, verticalAxis);
    }
}