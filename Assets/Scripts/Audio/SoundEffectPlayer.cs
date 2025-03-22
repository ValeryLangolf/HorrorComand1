using System;
using UnityEngine;

public delegate void AudioFinishedCallback();

public class SoundEffectPlayer : MonoBehaviour
{
    [SerializeField] private AudioSourcePrefab _prefab;

    private Pool<AudioSourcePrefab> _pool;

    private void Awake() =>
        _pool = new(_prefab, transform);

    public void Play(SoundParams soundParams)
    {
        if (soundParams.Clip == null)
            throw new ArgumentNullException(soundParams.Clip.name, "AudioClipNotFound");

        AudioSourcePrefab source = _pool.Get();
        source.Play(soundParams);
    }
}