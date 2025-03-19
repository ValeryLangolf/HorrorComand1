using UnityEngine;

public struct SoundParams
{
    private readonly SoundName _name;
    private readonly Transform _targetTransform;
    private readonly AudioFinishedCallback _callback;
    private AudioClip _audioClip;

    public SoundParams(SoundName name, Transform targetTransform, AudioFinishedCallback callback = null, AudioClip audioClip = null)
    {
        _name = name;
        _targetTransform = targetTransform;
        _callback = callback;
        _audioClip = audioClip;
    }

    public readonly SoundName Name => _name;

    public readonly Transform TargetTransform => _targetTransform;

    public readonly AudioFinishedCallback Callback => _callback;

    public readonly AudioClip Clip => _audioClip;

    public void SetClip(AudioClip audioClip)
    {
        _audioClip = audioClip;
    }
}