using UnityEngine;

public class PlayerAnimatorWrapping : CharacterAnimatorWrapping
{
    public const string GettingUp = nameof(GettingUp);
    public const string Sitting = nameof(Sitting);
    public const string SitToStand = nameof(SitToStand);
    public const string IsShowingBook = nameof(IsShowingBook);
    public const string ShowingBook = nameof(ShowingBook);
    public const string HidingBook = nameof(HidingBook);

    public PlayerAnimatorWrapping(Animator animator) : base(animator) { }

    public void ShowGettingUp(AnimationFinishedCallback callback = null) =>
        Play(GettingUp, callback);

    public void ShowSitting(AnimationFinishedCallback callback = null) =>
        SetTrigger(Sitting, callback, Sitting);

    public void ShowSitToStand(AnimationFinishedCallback callback = null) =>
        SetTrigger(SitToStand, callback, SitToStand);

    public void ShowBook(AnimationFinishedCallback callback = null) =>
        SetBool(IsShowingBook, true, callback, ShowingBook);

    public void HideBook(AnimationFinishedCallback callback = null) =>
        SetBool(IsShowingBook, false, callback, HidingBook);
}