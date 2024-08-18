using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioMixerLevels : MonoBehaviour
{
    public string parameter;
    public Slider slider;

    private void Awake()
    {
        slider.onValueChanged.AddListener(value =>
        {
            AudioManager.Instance.AdjustLevels(parameter, value);
        });
    }

    private void OnEnable()
    {
        slider.value = AudioManager.Instance.GetLevel(parameter);
    }
}
