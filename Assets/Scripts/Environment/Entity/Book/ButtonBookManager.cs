using System;

public class ButtonBookManager
{
    private readonly ButtonArrow[] _buttons;
    private readonly Action<ButtonClickListener> _onButtonPressed;

    public ButtonBookManager(ButtonArrow[] buttons, Action<ButtonClickListener> onButtonPressed)
    {
        _buttons = buttons;
        _onButtonPressed = onButtonPressed;
    }

    public void SubscribeClick()
    {
        foreach (var button in _buttons)
            button.ButtonPressed += _onButtonPressed;
    }

    public void UnsubscribeClick()
    {
        foreach (var button in _buttons)
            button.ButtonPressed -= _onButtonPressed;
    }

    public void UpdateButtonVisibility(int currentPageIndex, int pageCount)
    {
        if (currentPageIndex == 0)
            HideButton<ArrowLeft>();
        else
            ShowButton<ArrowLeft>();

        if (currentPageIndex == pageCount - 1)
            HideButton<ArrowRight>();
        else
            ShowButton<ArrowRight>();
    }

    private void ShowButton<T>() where T : ButtonClickListener
    {
        foreach (var button in _buttons)
            if (button is T searchButton)
                searchButton.ShowObject();
    }

    private void HideButton<T>() where T : ButtonClickListener
    {
        foreach (var button in _buttons)
            if (button is T searchButton)
                searchButton.HideObject();
    }
}