using System;
using UnityEngine;

[Serializable]
public class AudioController
{
    // Attributes
    public Audio[] audios;

    // Constructors
    public AudioController(GameObject gameObject)
    {
        if(audios == null)
        {
            audios = new Audio[0];
        }
        
        foreach (Audio audio in audios)
        {
            audio.source = gameObject.AddComponent<AudioSource>();
            audio.source.clip = audio.clip;

            audio.source.volume = audio.volume;
            audio.source.pitch = audio.pitch;
        }
    }

    // Functions
    public void Play (string name)
    {
        Audio audio = Array.Find(audios, audio => audio.name == name);
        
        if(audio == null)
        {
            Debug.LogWarning($"Audio not found: {name}");
            return;
        }

        audio.source.Play();
    }
}