using UnityEngine;

public class PlayerInput
{
    private const string Horizontal = "Horizontal";
    private const string Vertical = "Vertical";

    private const KeyCode _jump = KeyCode.Space;
    private const KeyCode _shift = KeyCode.LeftShift;
    private const KeyCode _interaction = KeyCode.E;
    private const KeyCode _book = KeyCode.Tab;

    public bool IsJumpPressed() =>
        Input.GetKeyDown(_jump);

    public bool IsMovePressed(out float valueRight, out float valueForward)
    {
        valueRight = Input.GetAxis(Horizontal);
        valueForward = Input.GetAxis(Vertical);

        return valueRight != 0 || valueForward != 0;
    }

    public bool IsShift() =>
        Input.GetKey(_shift);

    public bool IsInteractionButtonPressed() =>
        Input.GetKeyDown(_interaction);

    public bool IsBookPressed() =>
        Input.GetKeyDown(_book);
}