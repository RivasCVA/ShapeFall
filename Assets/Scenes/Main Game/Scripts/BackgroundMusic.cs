using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    public AudioClip musicClip;
    private AudioSource source;

    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
        source.clip = musicClip;
        source.loop = true;
        Invoke("PlayIfPossible", 1);
    }

    public void PlayIfPossible()
    {
        if (MusicButton.musicEnabled && !source.isPlaying)
        {
            source.Play();
        }
        else if (!MusicButton.musicEnabled && source.isPlaying)
        {
            source.Pause();
        }
    }

    public void Pause()
    {
        source.Pause();
    }

}
