using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    public Sound[] walkingSounds;
    public Sound[] runningSounds;

    void Awake()
    {
        SetupAudioSource(sounds);
        SetupAudioSource(walkingSounds);
        SetupAudioSource(runningSounds);
    }

    public void Play (string name)
    {
        var sound = Array.Find(sounds, sound => sound.soundName == name);
        sound.source.Play();
    }

    public void PlayStepSound()
    {
        var source = RandomAudio(walkingSounds);
        source.Play();
    }

    public void PlayRunSound()
    {
        var source = RandomAudio(runningSounds);
        source.Play();
    }

    private AudioSource RandomAudio(Sound[] sounds)
    {
        var random = UnityEngine.Random.Range(0, sounds.Length);
        var sound = sounds[random];

        return sound.source;
    }

    private void SetupAudioSource(Sound[] sounds)
    {
        foreach (Sound sound in sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;
            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
        }
    }
}
