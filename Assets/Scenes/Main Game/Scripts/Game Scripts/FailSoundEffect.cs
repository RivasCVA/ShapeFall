using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FailSoundEffect : MonoBehaviour
{
    public AudioClip failClip;
    public AudioSource source;

    public void PlayIfPossible()
    {
        if (SoundEffectsButton.soundEffectsEnabled)
        {
            source.clip = failClip;
            source.Play();
        }
    }

    public void Stop()
    {
        if (SoundEffectsButton.soundEffectsEnabled)
        {
            source.Stop();
        }
    }

    public void Pause()
    {
        if (SoundEffectsButton.soundEffectsEnabled)
        {
            source.Pause();
        }
    }
}
