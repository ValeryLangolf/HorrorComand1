using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioSourcePrefab : MonoBehaviour, IDeactivatable<AudioSourcePrefab>
{
    private AudioSource _source;

    public event Action<AudioSourcePrefab> Deactivated;

    private void Awake() =>
        _source = GetComponent<AudioSource>();

    public void Play(SoundParams soundParams)
    {
        _source.clip = soundParams.Clip;
        _source.Play();

        StartCoroutine(ReturnToQueue(soundParams));
    }

    private IEnumerator ReturnToQueue(SoundParams soundParams)
    {
        while (_source.isPlaying)
        {
            transform.position = soundParams.TargetTransform.position;

            yield return null;
        }

        soundParams.Callback?.Invoke();
        Deactivated?.Invoke(this);
    }
}