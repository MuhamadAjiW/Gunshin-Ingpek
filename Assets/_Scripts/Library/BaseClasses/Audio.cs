using System;
using UnityEngine;

[Serializable]
public class Audio
{
    // Attributes
    [HideInInspector] public AudioSource source;
    public string name;
    public AudioClip clip;
    public float volume;
    public float pitch;
    public bool loop;
    public bool spatialize;
}