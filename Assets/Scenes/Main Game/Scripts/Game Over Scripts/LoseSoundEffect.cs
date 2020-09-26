using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseSoundEffect : MonoBehaviour
{
	public AudioClip loseClip;
	public AudioSource source;

	public void PlayIfPossible()
	{
		if (SoundEffectsButton.soundEffectsEnabled)
		{
			source.clip = loseClip;
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
