using UnityEngine;

public class MusicalCenter : HintTrigger
{
    [SerializeField] private AudioSource _source;

    private bool _isPlaying;

    public void Play()
    {
        _isPlaying = !_isPlaying;

        if (_isPlaying)
            _source.Play();
        else
            _source.Pause();
    }
}