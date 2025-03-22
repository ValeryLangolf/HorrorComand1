using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class VolumeTransmitter : MonoBehaviour
{
    [SerializeField] private AudioMixer _mixer;
    [SerializeField] private List<SliderChangeInformer> _sliders;

    private ModifierVolume _modifier;

    private void Awake() =>
        _modifier = new ModifierVolume(_mixer);

    private void Start() =>
        InitSliders();

    private void InitSliders()
    {
        foreach (SliderChangeInformer slider in _sliders)
            slider.Init();
    }

    private void OnEnable()
    {
        foreach (SliderChangeInformer slider in _sliders)
            slider.ValueChanged += Transmit;
    }

    private void OnDisable()
    {
        foreach (SliderChangeInformer slider in _sliders)
            slider.ValueChanged -= Transmit;
    }

    private void Transmit(SliderChangeInformer slider, float value)
    {
        if (slider == null)
            throw new ArgumentNullException("Отсутствует слайдер");

        string group = string.Empty;

        if (slider is OverallSlider _)
            group = Params.AudioMixer.Master;

        SetLevel(group, value);
    }

    private void SetLevel(string group, float value) =>
        _modifier.SetLevel(group, value);
}