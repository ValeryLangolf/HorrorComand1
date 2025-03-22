using UnityEngine;

public class PlotNote : ButtonClickInformer 
{
    private Transform _defaultPosition;

    public Transform DefaultPosition => _defaultPosition;

    public void Init(Transform defaultPosition)
    {
        _defaultPosition = defaultPosition;
    }
}