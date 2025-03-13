using UnityEngine;

public class SpeedSmoother
{
    private readonly float _speedChangeRate;
    private float _value;

    public SpeedSmoother(float speedChangeRate) =>
        _speedChangeRate = speedChangeRate;

    public float Smooth(float value)
    {
        _value = Mathf.MoveTowards(_value, value, Time.deltaTime * _speedChangeRate);

        return _value;
    }        
}