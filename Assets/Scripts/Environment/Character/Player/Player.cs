using UnityEngine;

public class Player : Character
{
    [SerializeField] private Book _book;

    private PlayerAnimatorWrapping _animatorWrapping;
    private bool _isShowedBook;

    protected override void Awake()
    {
        base.Awake();

        _animatorWrapping = new(GetAnimator);
        _book.HideObject();
    }

    public void ShowGettingUp(AnimationFinishedCallback callback = null) =>
        _animatorWrapping.ShowGettingUp(callback);

    public void ShowSitting(AnimationFinishedCallback callback = null) =>
        _animatorWrapping.ShowSitting(callback);

    public void ShowSitToStand(AnimationFinishedCallback callback = null) =>
        _animatorWrapping.ShowSitToStand(callback);

    public void ShowBook(AnimationFinishedCallback callback = null)
    {
        _book.ShowObject();
        _isShowedBook = true;
        _animatorWrapping.ShowBook(callback);
    }

    public void HideBook()
    {
        _isShowedBook = false;
        _animatorWrapping.HideBook();
    }

    public void DeactivateBook()
    {
        if (_isShowedBook == false)
            _book.HideObject();
    }
}