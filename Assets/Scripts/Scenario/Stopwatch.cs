using System.Collections;
using UnityEngine;

public class Stopwatch : MonoBehaviour
{
    private const float OneSecond = 1;

    private WaitForSeconds _wait;
    private bool _isOn;

    private int _timeInSeconds = 0;

    private void Awake()
    {
        _wait = new(OneSecond);
        _isOn = true;
        StartCoroutine(Tick());
    }

    public string GetTimeGame()
    {
        _isOn = false;

        int hours = _timeInSeconds / 3600;
        int minutes = (_timeInSeconds % 3600) / 60;
        int seconds = _timeInSeconds % 60;

        return $"{Normalize(hours)}:{Normalize(minutes)}:{Normalize(seconds)}";
    }

    private string Normalize(int value) =>
        value < 10 ? $"0{value}" : $"{value}";

    private IEnumerator Tick()
    {
        while(_isOn)
        {
            yield return _wait;

            ++_timeInSeconds;
        }
    }
}