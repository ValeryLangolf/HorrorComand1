using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
[RequireComponent(typeof(Image))]
public abstract class ButtonClickInformer : Entity
{
    private const float Alpha = 0.1f;

    private Button _button;
    private Image _image;

    public event Action<ButtonClickInformer> ButtonPressed;

    protected virtual void Awake()
    {
        _button = GetComponent<Button>();
        _image = GetComponent<Image>();
        _image.alphaHitTestMinimumThreshold = Alpha;
    }

    protected virtual void OnEnable() =>
        _button.onClick.AddListener(OnButtonClick);

    protected virtual void OnDisable() =>
        _button.onClick.RemoveListener(OnButtonClick);

    protected Color GetColor() =>
        _image.color;

    protected void SetColor(Color color) =>
        _image.color = color;

    protected virtual void OnButtonClick() =>
        ButtonPressed?.Invoke(this);
}