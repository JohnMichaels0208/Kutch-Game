using UnityEngine;
using UnityEngine.Audio;

public class SoundManagerScript : MonoBehaviour
{
    public const string soundEffectGroupName = "Sound Effects Volume";
    public const string musicGroupName = "Music Volume";
    public const string gameSoundGroupName = "Game Sound Volume";

    public static void PlaySound(AudioSource audioSource, Sound soundToPlay)
    {
        if (audioSource == null)
        {
            return;
        }
        audioSource.outputAudioMixerGroup = soundToPlay.group;
        audioSource.clip = soundToPlay.clip;
        audioSource.Play();
    }

    public static void UpdateAudioMixerGroupVolume(AudioMixer audioMixer, string audioGroupName, float volume)
    {
        audioMixer.SetFloat(audioGroupName, volume);
    }


    public static float SliderValueToAudioMixerGroupValue(float sliderValue)
    {
        return Mathf.Log10(sliderValue) * 20;
    }
}
