using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public AudioData[] audioList;
    public AudioData[] musicList;
    public AudioData mainMenuTheme;
    AudioData currentMusic;

    IEnumerator playNextMusicCoroutine;

    public float fadeDuration;

    public static AudioManager Instance { get; private set; }

    //Adds audio sources for sounds
    private void Awake()
    {
        Instance = this;

        for (int i = 0; i < audioList.Length; i++)
        {
            audioList[i].source = GetAudioSource(audioList[i]);
        }

        for (int i = 0; i < musicList.Length; i++)
        {
            musicList[i].source = GetAudioSource(musicList[i]);
        }

        mainMenuTheme.source = GetAudioSource(mainMenuTheme);
    }

    AudioSource GetAudioSource(AudioData data)
    {
        AudioSource source = gameObject.AddComponent<AudioSource>();
        source.volume = data.desiredVolume;
        source.clip = data.clip;
        return source;
    }

    //Helper function for buttons, plays click sound
    public void PlayClick(Button button) => PlaySound(button, AudioKey.Click);

    //Helper function for buttons, plays hover sound
    public void PlayHover(Button button) => PlaySound(button, AudioKey.Hover);

    //Plays click sound if button is enabled
    private void PlaySound(Button button, AudioKey soundKey)
    {
        if (button.interactable)
            PlaySound(soundKey);
    }

    public void PlaySound(AudioKey soundKey)
    {
        if (SettingsManager.Instance.SoundOn)
            Array.Find(audioList, d => d.key == soundKey).source.Play();
    }

    //If the sound settings have changed
    internal void OnSoundStateChanged(bool value)
    {
        
    }

    //If the music settings have changed, play or stop the music
    public void OnMusicStateChanged(bool newState)
    {
        if (newState)
            StartCoroutine(FadeMusicInOut(null, mainMenuTheme));
        else
        {
            StopCoroutine(playNextMusicCoroutine);
            currentMusic.source.Stop();
            currentMusic = null;
        }
    }

    //Play menu music
    public void OnMenuEnter()
    {
        if (!SettingsManager.Instance.MusicOn)
            return;

        if (currentMusic != null)
            StopCoroutine(playNextMusicCoroutine);
        StartCoroutine(FadeMusicInOut(currentMusic, mainMenuTheme));
    }

    //Stop menu music and play game music
    public void OnMenuExit()
    {
        if (!SettingsManager.Instance.MusicOn)
            return;

        if (currentMusic != null)
            StopCoroutine(playNextMusicCoroutine);
        StartCoroutine(FadeMusicInOut(mainMenuTheme, GetRandomMusic()));
    }

    //Get random game music
    AudioData GetRandomMusic()
    {
        int index = UnityEngine.Random.Range(0, musicList.Length);
        return musicList[index];
    }

    //Fade between the tracks, if either one is null, that fade part is skipped
    IEnumerator FadeMusicInOut(AudioData audioOut, AudioData audioIn)
    {
        float timeStep = 0.1f;
        float fadeSpeed;

        if (audioOut != null)
        {
            fadeSpeed = -audioOut.desiredVolume / fadeDuration * timeStep;
            yield return Fade(audioOut.source, fadeSpeed, timeStep);
            audioOut.source.Stop();
            currentMusic = null;
        }

        if (audioIn != null)
        {
            if (currentMusic != null) //to be sure the music won't be interrupted
                StopCoroutine(playNextMusicCoroutine);
            fadeSpeed = audioIn.desiredVolume / fadeDuration * timeStep;
            audioIn.source.volume = 0.0f;
            PlayMusic(audioIn);
            yield return Fade(audioIn.source, fadeSpeed, timeStep);
        }
    }

    //Helper function for FadeMusicInOut
    IEnumerator Fade(AudioSource source, float fadeSpeed, float timeStep)
    {
        for (float time = 0; time < fadeDuration; time += timeStep)
        {
            source.volume += fadeSpeed;
            yield return new WaitForSeconds(timeStep);
        }
    }

    //Sets the current music to this audio and plays it
    void PlayMusic(AudioData audio)
    {
        audio.source.Play();
        currentMusic = audio;
        playNextMusicCoroutine = PlayNextMusicOnEnd();
        StartCoroutine(playNextMusicCoroutine);
    }

    //Listens for the track end and plays the proper next one
    IEnumerator PlayNextMusicOnEnd()
    {
        yield return new WaitForSeconds(currentMusic.clip.length - fadeDuration);
        if (currentMusic.key == mainMenuTheme.key)
            StartCoroutine(FadeMusicInOut(mainMenuTheme, mainMenuTheme));
        else
            StartCoroutine(FadeMusicInOut(currentMusic, GetRandomMusic()));
    }
}

//Used to identify audio tracks
[System.Serializable]
public enum AudioKey
{
    MainMenu,
    Track1,
    Track2,
    Track3,
    Track4,
    Click,
    Hover,
    Bouncer,
    Triangle,
    Spike,
    Barrel,
    Attack
}

//Stores audio source and track data
[System.Serializable]
public class AudioData
{
    public AudioKey key;
    public float desiredVolume;
    public AudioClip clip;

    [HideInInspector]
    public AudioSource source;
}
