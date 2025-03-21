using System;
using System.Collections.Generic;
using UnityEngine;

public enum SoundName
{
    LeftStepGrass,
    RightStepGrass,
    TurningPage,
    NoteOpened,
    NoteClosed,
}

public class SoundTransmitter : MonoBehaviour
{
    [SerializeField] private SoundEffectPlayer _sfxPlayer;
    [SerializeField] private Player _player;
    [SerializeField] private Book _book;

    [Header("Sounds:")]
    [SerializeField] private AudioClip _leftStepGrass;
    [SerializeField] private AudioClip _rightStepGrass;
    [SerializeField] private AudioClip _turningPage;
    [SerializeField] private AudioClip _noteOpened;
    [SerializeField] private AudioClip _noteClosed;

    private Dictionary<SoundName, AudioClip> _clips;

    private void Awake() =>
        InitDictionary();

    private void OnEnable()
    {
        _player.SoundPlayBack += Play;
        _book.SoundPlayBack += Play;
    }

    private void OnDisable()
    {
        _player.SoundPlayBack -= Play;
        _book.SoundPlayBack -= Play;
    }

    public void Play(SoundParams soundParams)
    {
        soundParams.SetClip(FindClip(soundParams.Name));
        _sfxPlayer.Play(soundParams);
    }

    private void InitDictionary()
    {
        _clips = new()
        {
            { SoundName.LeftStepGrass, ValidateClip(_leftStepGrass) },
            { SoundName.RightStepGrass, ValidateClip(_rightStepGrass) },
            { SoundName.TurningPage, ValidateClip(_turningPage) },
            { SoundName.NoteOpened, ValidateClip(_noteOpened) },
            { SoundName.NoteClosed, ValidateClip(_noteClosed) },
        };
    }

    private AudioClip ValidateClip(AudioClip clip)
    {
        if (clip == null)
            throw new ArgumentNullException(clip.name, "AudioClipNotFound");

        return clip;
    }

    private AudioClip FindClip(SoundName name)
    {
        if (_clips.TryGetValue(name, out AudioClip clip) == false)
            throw new ArgumentNullException(nameof(_clips), "AudioClipNotFoundDictionary");

        return clip;
    }
}