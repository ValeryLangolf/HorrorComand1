using System;
using UnityEngine;

public delegate void AudioFinishedCallback();

public class SoundEffectPlayer : MonoBehaviour
{
    [SerializeField] private SoundEffectSource _prefab;

    private Pool<SoundEffectSource> _pool;

    private void Awake() =>
        _pool = new(_prefab, transform);

    public void Play(SoundParams soundParams)
    {
        if (soundParams.Clip == null)
            throw new ArgumentNullException(soundParams.Clip.name, "AudioClipNotFound");

        SoundEffectSource soundEffectSource = _pool.Get();
        soundEffectSource.Play(soundParams);
    }
}