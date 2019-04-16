using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundHandler : MonoBehaviour
{
    public static SoundHandler soundHandler;
    private AudioSource audioSource;

    private static bool created = false;

    private void Awake()
    {
        SoundHandlerInstanceCheck();
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        soundHandler = GetComponent<SoundHandler>();
    }

    public void PlaySoundFX(AudioSource source, AudioClip sound)
    {
        source.PlayOneShot(sound);
    }

    public void PlayMusic(AudioClip sound)
    {
        audioSource.Play();
    }

    public void StopSoundFX(AudioSource source)
    {
        source.Stop();
    }

    private void SoundHandlerInstanceCheck()
    {
        if (!created)
        {
            created = true;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
