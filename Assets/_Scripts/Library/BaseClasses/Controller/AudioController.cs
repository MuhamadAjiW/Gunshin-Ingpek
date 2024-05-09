using System;
using UnityEngine;

[Serializable]
public class AudioController
{
    // Attributes
    public Audio[] audios;

    // Constructors
    public void Init(MonoBehaviour monoBehaviour)
    {
        foreach (Audio audio in audios)
        {
            audio.source = monoBehaviour.gameObject.AddComponent<AudioSource>();
            audio.source.clip = audio.clip;

            audio.source.volume = audio.volume;
            audio.source.pitch = audio.pitch;
            audio.source.playOnAwake = false;
            audio.source.loop = audio.loop;
            audio.source.spatialize = audio.spatialize;
        }
    }

    // Functions
    public string Play (string name)
    {
        Audio audio = Array.Find(audios, audio => audio.name == name);
        
        if(audio == null)
        {
            Debug.LogWarning($"Audio not found: {name}");
            return null;
        }

        audio.source.Play();

        return name;
    }

    public void Stop (string name)
    {
        Audio audio = Array.Find(audios, audio => audio.name == name);
        
        if(audio == null)
        {
            Debug.LogWarning($"Audio not found: {name}");
            return;
        }

        audio.source.Stop();
    }

    public void Mute (string name)
    {
        Audio audio = Array.Find(audios, audio => audio.name == name);
        
        if(audio == null)
        {
            Debug.LogWarning($"Audio not found: {name}");
            return;
        }

        audio.source.volume = 0;
    }

    public void Unmute (string name)
    {
        Audio audio = Array.Find(audios, audio => audio.name == name);
        
        if(audio == null)
        {
            Debug.LogWarning($"Audio not found: {name}");
            return;
        }

        audio.source.volume = 1;
    }
}