using UnityEngine;

public class Player : Character
{
    [SerializeField] private Book _book;

    private PlayerAnimatorWrapping _animatorWrapping;

    protected override void Awake()
    {
        base.Awake();
        _animatorWrapping = new(GetAnimator);        
    }

    private void Start() =>
        _book.HideObject();

    public void ShowBook(CallbackFinished callback)
    {        
        _animatorWrapping.ShowBook(callback);
        PlaySound(SoundName.BookOpened);
    }

    public void HideBook(CallbackFinished callback)
    {
        _animatorWrapping.HideBook(callback);
        PlaySound(SoundName.BookClosed);
    }
}