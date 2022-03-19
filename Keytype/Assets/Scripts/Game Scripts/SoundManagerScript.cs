using UnityEngine;
using UnityEngine.Audio;

public class SoundManagerScript : MonoBehaviour
{
    private const string soundEffectGroupName = "Sound Effects";
    private const string musicGroupName = "Music";

    [SerializeField] private AudioMixer audioMixer;
    public void PlaySound(AudioSource audioSource, Sound soundToPlay)
    {
        audioSource.outputAudioMixerGroup = soundToPlay.group;
        audioSource.clip = soundToPlay.clip;
        audioSource.Play();
    }

    public void UpdateGameVolume()
    {
    }
}
