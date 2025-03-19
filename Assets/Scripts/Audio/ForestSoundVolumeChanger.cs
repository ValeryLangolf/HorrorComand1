using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class ForestSoundVolumeChanger : MonoBehaviour
{
    [SerializeField] private AudioSource _source;
    [SerializeField, Range(0, 1)] private float _targetVolume;
    [SerializeField] private float _speed;
    private Coroutine _coroutine;

    private void OnTriggerEnter(Collider other)
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(ChangeVolumeOverTime());
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