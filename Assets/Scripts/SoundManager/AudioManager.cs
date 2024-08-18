using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;
    public static AudioManager Instance => instance;

    public string[] audioParameters;
    public AudioMixer audioMixer;
    
    private void Start()
    {
        if (instance != null) Destroy(gameObject);
        DontDestroyOnLoad(this);
        instance = this;
        foreach (var parameter in audioParameters)
        {
            Debug.Log(parameter + " " + GetPlayerPrefsValue(parameter));
            audioMixer.SetFloat(parameter, GetPlayerPrefsValue(parameter));
        }
    }

    private string GetPlayerPrefsKey(string key)
    {
        return "audio::" + key;
    }

    private float GetPlayerPrefsValue(string key)
    {
        return PlayerPrefs.GetFloat(GetPlayerPrefsKey(key), 0);
    }

    public void AdjustLevels(string parameter, float level)
    {
        PlayerPrefs.SetFloat(GetPlayerPrefsKey(parameter), level);
        audioMixer.SetFloat(parameter, level);
    }

    public float GetLevel(string parameter)
    {
        return GetPlayerPrefsValue(parameter);
    }
}
