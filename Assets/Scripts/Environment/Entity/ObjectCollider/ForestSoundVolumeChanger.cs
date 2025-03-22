using System.Collections;
using UnityEngine;

public class ForestSoundVolumeChanger : ObjectCollider
{
    [SerializeField] private ForestSoundVolumeChanger _other;
    [SerializeField] private AudioSource _source;
    [SerializeField, Range(0, 1)] private float _targetVolume;
    [SerializeField] private float _speed;
    private Coroutine _coroutine;

    private void Start() =>
        SetColliderAsTrigger();

    private void OnTriggerEnter(Collider other)
    {
        _other.StopRutine();
        StopRutine();
        _coroutine = StartCoroutine(ChangeVolumeOverTime());
    }

    public void StopRutine()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);
    }

    private IEnumerator ChangeVolumeOverTime()
    {
        while (_source.volume != _targetVolume)
        {
            _source.volume = Mathf.MoveTowards(_source.volume, _targetVolume, _speed * Time.deltaTime);

            yield return null;
        }
    }
}