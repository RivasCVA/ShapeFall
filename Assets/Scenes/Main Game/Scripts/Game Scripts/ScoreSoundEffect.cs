using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreSoundEffect : MonoBehaviour
{
	public AudioClip scoreClip;
	public AudioSource source;

	public void PlayIfPossible()
	{
		if (SoundEffectsButton.soundEffectsEnabled)
		{
            source.clip = scoreClip;
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
