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

    public void ShowBook(CallbackFinished callback) =>
        SetBool(IsShowingBook, true, callback, ShowingBook);

    public void HideBook(CallbackFinished callback = null) =>
        SetBool(IsShowingBook, false, callback, HidingBook);
}