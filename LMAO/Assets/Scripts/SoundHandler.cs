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

    private void Start()
    {
        audioSource = this.GetComponent<AudioSource>();
        soundHandler = this.GetComponent<SoundHandler>();
    }

    public void PlaySoundFX(AudioClip sound)
    {
        audioSource.PlayOneShot(sound);
    }

    public void PlayMusic(AudioClip sound)
    {
        audioSource.Play();
    }
}
