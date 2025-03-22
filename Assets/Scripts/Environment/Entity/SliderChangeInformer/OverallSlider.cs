using UnityEngine;

public class OverallSlider : SliderChangeInformer 
{
    [SerializeField] private EarSizer _earSizer;

    protected override void OnChange(float value)
    {
        base.OnChange(value);
        _earSizer.ResizeByVolume(value);
    }
}