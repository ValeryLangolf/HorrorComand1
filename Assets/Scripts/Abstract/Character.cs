using UnityEngine;

public abstract class Character : MonoBehaviour
{
    private SoundEffectPlayer _soundPlayer;

    protected virtual void Awake() =>
        _soundPlayer = new(this);

    public void PlaySound(AudioClip clip, AudioFinishedCallback callback = null) =>
        _soundPlayer.Play(clip, callback);
}