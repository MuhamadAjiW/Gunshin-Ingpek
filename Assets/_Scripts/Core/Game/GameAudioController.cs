using System.Collections;
using UnityEngine;

public class GameAudioController : MonoBehaviour
{
    public static GameAudioController Instance;
    public AudioController audioController;

    protected void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        DontDestroyOnLoad(this);
        audioController.Init(this);
    }

    public void PlayOnce(Audio audio, float volume = 1)
    {
        audio.source = gameObject.AddComponent<AudioSource>();
        audio.source.clip = audio.clip;

        audio.source.volume = audio.volume;
        audio.source.pitch = audio.pitch;
        audio.source.spatialize = audio.spatialize;
        audio.source.playOnAwake = false;

        audio.source.Play();

        StartCoroutine(DeleteClipWhenComplete(audio.source));
    }

    public void PlayOnce(AudioClip clip, float volume = 1, float pitch = 1, bool spatialize = false)
    {
        AudioSource audio = gameObject.AddComponent<AudioSource>();
        audio.clip = audio.clip;

        audio.volume = volume;
        audio.pitch = pitch;
        audio.spatialize = false;

        audio.Play();
        
        StartCoroutine(DeleteClipWhenComplete(audio));
    }

    public IEnumerator DeleteClipWhenComplete(AudioSource audio)
    {
        yield return new WaitForSeconds(audio.clip.length);

        Destroy(audio);
    }
}