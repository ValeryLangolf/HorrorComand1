using System;

public class ButtonBookManager
{
    private readonly ButtonClickListener[] _buttons;
    private readonly Action<ButtonClickListener> _onButtonPressed;

    public ButtonBookManager(ButtonClickListener[] buttons, Action<ButtonClickListener> onButtonPressed)
    {
        _buttons = buttons;
        _onButtonPressed = onButtonPressed;
    }

    public void EnableButtons()
    {
        foreach (var button in _buttons)
            button.ButtonPressed += _onButtonPressed;
    }

    public void DisableButtons()
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