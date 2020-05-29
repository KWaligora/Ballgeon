using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    private bool soundOn;
    private bool musicOn;

    public static SettingsManager Instance { get; private set; }

    public bool SoundOn { 
        get => soundOn; 
        set 
        {
            soundOn = value;
            if (AudioManager.Instance != null)
                AudioManager.Instance.OnSoundStateChanged(value);
        } 
    }

    public bool MusicOn { 
        get => musicOn; 
        set 
        {
            musicOn = value;
            if (AudioManager.Instance != null)
                AudioManager.Instance.OnMusicStateChanged(value);
        } 
    }

    public void Awake()
    {
        Instance = this;
    }
}
