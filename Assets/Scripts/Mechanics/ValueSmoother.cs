using UnityEngine;

public class ValueSmoother
{
    private readonly float _speedChangeRate;
    private float _value;

    public ValueSmoother(float speedChangeRate)
    {
        _speedChangeRate = speedChangeRate;
    }

    public float Smooth(float value)
    {
        _value = Mathf.MoveTowards(_value, value, Time.deltaTime * _speedChangeRate);

        return _value;
    }
}