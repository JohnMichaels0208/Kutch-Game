using UnityEngine;

public class SoundManagerScript : MonoBehaviour
{
    public void PlaySound(AudioSource audioSource, Sound soundToPlay)
    {
        audioSource.outputAudioMixerGroup = soundToPlay.group;
        audioSource.clip = soundToPlay.clip;
        audioSource.Play();
    }
}
