using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonArrow : ButtonClickListener, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Color _enterColor;

    private Color _defaultColor = Color.black;

    private void Start() =>
        _defaultColor = GetColor();

    protected override void OnDisable()
    {
        base.OnDisable();
        if (_defaultColor == Color.black)
            return;

        SetColor(_defaultColor);
    }

    public void OnPointerEnter(PointerEventData eventData) =>
        SetColor(_enterColor);

    public void OnPointerExit(PointerEventData eventData) =>
        SetColor(_defaultColor);
}