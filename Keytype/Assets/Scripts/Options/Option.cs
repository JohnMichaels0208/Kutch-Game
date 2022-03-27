using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.Audio;

public class Option : MonoBehaviour
{
    [HideInInspector] public float soundEffectsVolume = 0;
    public bool isFullScreen = true;
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider soundEffectsVolumeSlider;
    [SerializeField] private Toggle isFullScreenToggle;

    public void SaveOption()
    {
        SoundManagerScript.UpdateAudioMixerGroupVolume(audioMixer);
        Screen.fullScreen = isFullScreen;
        SaveSystemScript.SaveOption(this);
    }

    public void LoadOption()
    {
        Debug.Log("Loading option");
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

    public void UpdateOptionVolume(Slider slider)
    {
        soundEffectsVolume = slider.value;
    }

    public void UpdateOptionIsFullScreen(Toggle toggle)
    {
        isFullScreen = toggle.isOn;
    }
}