using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;
using System;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Button))]
public class ButtonEscap : Entity, IPointerEnterHandler, IPointerExitHandler
{
    private const string IsMouseEnter = nameof(IsMouseEnter);

    private Animator _animator;
    private Button _button;

    public event Action<ButtonEscap> ButtonPressed;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _button = GetComponent<Button>();
    }

    private void OnEnable() =>
        _button.onClick.AddListener(OnButtonClick);

    private void OnDisable() =>
        _button.onClick.RemoveListener(OnButtonClick);

    public void OnPointerEnter(PointerEventData eventData) =>
        _animator.SetBool(IsMouseEnter, true);

    public void OnPointerExit(PointerEventData eventData) =>
        _animator.SetBool(IsMouseEnter, false);

    protected virtual void OnButtonClick() =>
        ButtonPressed?.Invoke(this);
}