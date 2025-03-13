using UnityEngine;

public class BookAnimator
{
    public const string Show = nameof(Show);
    public const string Hide = nameof(Hide);

    private readonly Animator _animator;

    public BookAnimator(Animator animator) =>
        _animator = animator;

    public void ShowBook() =>
        _animator.SetTrigger(Show);

    public void HideBook() =>
        _animator.SetTrigger(Hide);
}