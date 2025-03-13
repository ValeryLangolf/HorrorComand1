using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void AudioFinishedCallback();

public class SoundEffectPlayer
{
    private readonly MonoBehaviour _monobehaviour;
    private readonly List<AudioSource> _sources = new();
    private readonly Queue<AudioSource> _freeSources = new();

    public SoundEffectPlayer(MonoBehaviour monobehaviour)
    {
        _monobehaviour = monobehaviour;
    }

    public void Play(AudioClip clip, AudioFinishedCallback callback = null)
    {
        if (clip == null)
            throw new ArgumentNullException(clip.name, "AudioClipNotFound");

        AudioSource source = Get();

        source.PlayOneShot(clip);
        _monobehaviour.StartCoroutine(ReturnToQueue(source, callback));
    }

    private AudioSource Get()
    {
        if (_freeSources.Count > 0)
            return _freeSources.Dequeue();

        AudioSource source = _monobehaviour.gameObject.AddComponent<AudioSource>();
        source.playOnAwake = false;
        source.spatialBlend = 1f;
        _sources.Add(source);

        return source;
    }

    private IEnumerator ReturnToQueue(AudioSource source, AudioFinishedCallback callback = null)
    {
        yield return new WaitWhile(() => source.isPlaying);

        _freeSources.Enqueue(source);
        callback?.Invoke();
    }
}