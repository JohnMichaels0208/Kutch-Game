using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class Option : MonoBehaviour
{
    [HideInInspector] public float soundEffectsVolume = 1;
    [HideInInspector] public float gameSoundVolume = 1;
    public bool isFullScreen = true;
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider soundEffectsVolumeSlider;
    [SerializeField] private Toggle isFullScreenToggle;

    public void SaveOption()
    {
        SoundManagerScript.UpdateAudioMixerGroupVolume(audioMixer, SoundManagerScript.gameSoundGroupName, gameSoundVolume);
        SoundManagerScript.UpdateAudioMixerGroupVolume(audioMixer, SoundManagerScript.soundEffectGroupName, soundEffectsVolume);
        Screen.fullScreen = isFullScreen;
        SaveSystemScript.SaveOption(this);
    }

    public void LoadOption()
    {
        OptionData optionData = SaveSystemScript.LoadOption();
        soundEffectsVolume = optionData.soundEffectsVolume;
        isFullScreen = optionData.isFullScreen;
        soundEffectsVolumeSlider.value = soundEffectsVolume;
        isFullScreenToggle.isOn = isFullScreen;
    }

    private void OnEnable()
    {
        LoadOption();
    }

    public void UpdateSoundEffectsOptionVolume(Slider slider)
    {
        soundEffectsVolume = SoundManagerScript.SliderValueToAudioMixerGroupValue(slider.value);
    }

    public void UpdateGameSoundOptionVolume(Slider slider)
    {
        gameSoundVolume = SoundManagerScript.SliderValueToAudioMixerGroupValue(slider.value);
    }

    public void UpdateOptionIsFullScreen(Toggle toggle)
    {
        isFullScreen = toggle.isOn;
    }
}