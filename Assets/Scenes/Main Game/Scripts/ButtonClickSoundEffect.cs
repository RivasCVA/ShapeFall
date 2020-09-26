using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonClickSoundEffect : MonoBehaviour
{
    public AudioClip buttonClickClip;
    public AudioSource source;

    // Start is called before the first frame update
    void Start()
    {
        source.clip = buttonClickClip;
    }

    public void PlayIfPossible()
    {
        if (SoundEffectsButton.soundEffectsEnabled)
        {
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
