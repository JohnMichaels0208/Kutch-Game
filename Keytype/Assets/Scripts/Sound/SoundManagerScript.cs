using UnityEngine;
using UnityEngine.Audio;

public class SoundManagerScript : MonoBehaviour
{
    private const string soundEffectGroupName = "Sound Effects Volume";
    private const string musicGroupName = "Music Volume";

    public static void PlaySound(AudioSource audioSource, Sound soundToPlay)
    {
        audioSource.outputAudioMixerGroup = soundToPlay.group;
        audioSource.clip = soundToPlay.clip;
        audioSource.Play();
    }

    public static void UpdateAudioMixerGroupVolume(AudioMixer audioMixer)
    {
        audioMixer.SetFloat(soundEffectGroupName, Mathf.Log10(SaveSystemScript.LoadOption().soundEffectsVolume)*20);
    }
}
