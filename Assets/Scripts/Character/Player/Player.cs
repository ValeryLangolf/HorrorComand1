using UnityEngine;

public class Player : Character
{
    [SerializeField] private Book _book;

    private PlayerAnimatorWrapping _animatorWrapping;
    private bool _isShowBook;

    protected override void Awake()
    {
        base.Awake();

        _animatorWrapping = new(this, Animator);
        _book.HideObject();
    }

    public void ShowGettingUp(AnimationFinishedCallback callback = null) =>
        _animatorWrapping.ShowGettingUp(callback);

    public void ShowSitting(AnimationFinishedCallback callback = null) =>
        _animatorWrapping.ShowSitting(callback);

    public void ShowSitToStand(AnimationFinishedCallback callback = null) =>
        _animatorWrapping.ShowSitToStand(callback);

    public void ShowBook()
    {
        _book.ShowObject();
        _isShowBook = true;
        _animatorWrapping.ShowBook();
    }

    public void HideBook()
    {
        _isShowBook = false;
        _animatorWrapping.HideBook();
    }

    public void DeactivateBook()
    {
        if (_isShowBook == false)
            _book.HideObject();
    }
}