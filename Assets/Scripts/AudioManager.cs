using UnityEngine.Audio;
using UnityEngine;
using System;
using UnityEngine.UI;

[System.Serializable]
public class Sound
{
    public string name;

    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume;

    [Range(.1f, 3f)]
    public float pitch;

    public bool loop;

    public bool music;

    [HideInInspector]
    public AudioSource source;
}

public class AudioManager : MonoBehaviour
{
    public Slider musicControl;
    public Slider effectsControl;

    private GameDataConfig saving;

    public Sound[] sounds;
    
    public static AudioManager instance;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        //DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    public void Start()
    {
        saving = DBOperationsController.element.LoadSaving();

        MusicVolumeControl(saving.musicLevel / 3);
        EffectsVolumeControl(saving.effectsLevel / 3);
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
        }
        s.source.Play();
    }

    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
        }
        s.source.Stop();
    }

    public void MusicVolumeControl(float volume)
    {
        foreach (Sound s in sounds)
        {
            if (s.music)
            {
                s.source.volume = volume;
            }
        }
    }

    public void EffectsVolumeControl(float volume)
    {
        foreach (Sound s in sounds)
        {
            if (!s.music)
            {
                s.source.volume = volume;
            }
        }
    }

    public void PlayRandomTrack()
    {
        int index = UnityEngine.Random.Range(1, 4);

        Play("track-" + index);
    }
}
