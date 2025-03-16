using UnityEngine;

public class CharacterAnimatorWrapping : AnimatorWrapping
{
    private const string RightMoving = nameof(RightMoving);
    private const string ForwardMoving = nameof(ForwardMoving);
    private const string Jump = nameof(Jump);

    public CharacterAnimatorWrapping(Animator animator) : base(animator) { }

    public void ShowMove(float right, float forward)
    {
        SetFloat(RightMoving, right);
        SetFloat(ForwardMoving, forward);
    }

    public void ShowJump() =>
        SetTrigger(Jump);
}