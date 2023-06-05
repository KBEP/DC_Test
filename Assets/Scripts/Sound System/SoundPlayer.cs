using System.Collections.Generic;
using UnityEngine;

public sealed class SoundPlayer : MonoBehaviour, ISoundPlayerAccessor
{
	LinkedList<AudioSource> source = new LinkedList<AudioSource>();

	public void PlayOneShot (AudioClip clip)
	{
		if (clip == null) return;
		AudioSource src = GetFree();
		if (src == null) return;
		src.PlayOneShot(clip);
	}

	public void PlayOneShot (string clipPath)
	{
		AudioClip clip = Resources.Load<AudioClip>(clipPath);
		PlayOneShot(clip);
	}

	AudioSource GetFree ()
	{
		foreach (AudioSource s in source) if (!s.isPlaying) return s;
		AudioSource newSource = gameObject.AddComponent<AudioSource>();
		if (newSource == null) return null;
		newSource.playOnAwake = false;
		source.AddLast(newSource);
		return newSource;
	}
}
