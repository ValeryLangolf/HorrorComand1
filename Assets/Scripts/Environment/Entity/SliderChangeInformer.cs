using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public abstract class SliderChangeInformer : Entity
{
    private Slider _slider;

    public event Action<SliderChangeInformer, float> ValueChanged;

    public void Init()
    {
        if (_slider == null)
            _slider = GetComponent<Slider>();

        OnChange(_slider.value);
    }

    private void OnEnable() =>
        _slider.onValueChanged.AddListener(OnChange);

    private void OnDisable() =>
        _slider.onValueChanged.RemoveListener(OnChange);

    protected virtual void OnChange(float value) =>
        ValueChanged?.Invoke(this, value);
}