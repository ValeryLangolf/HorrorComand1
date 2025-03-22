using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioSourcePrefab : MonoBehaviour, IDeactivatable<AudioSourcePrefab>
{
    private AudioSource _source;
    private Coroutine _coroutine;

    public event Action<AudioSourcePrefab> Deactivated;

    private void Awake() =>
        _source = GetComponent<AudioSource>();

    public void Play(SoundParams soundParams)
    {
        _source.clip = soundParams.Clip;
        _source.Play();

        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(ReturnToPoolOverTime(soundParams));
    }

    private IEnumerator ReturnToPoolOverTime(SoundParams soundParams)
    {
        while (_source.isPlaying)
        {
            transform.position = soundParams.TargetTransform.position;

            yield return null;
        }

        if (soundParams.Callback != null)
            soundParams.Callback?.Invoke();

        Deactivated?.Invoke(this);
    }
}