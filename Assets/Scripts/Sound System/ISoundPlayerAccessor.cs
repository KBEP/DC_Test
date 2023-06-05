using UnityEngine;

public interface ISoundPlayerAccessor
{
	public void PlayOneShot (AudioClip clip);
	public void PlayOneShot (string clipPath);
}
