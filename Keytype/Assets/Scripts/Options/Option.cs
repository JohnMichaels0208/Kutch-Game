using UnityEngine;
using UnityEngine.UI;
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
        Debug.Log(soundEffectsVolume + "   |   " + Mathf.Log10(soundEffectsVolume) * 20);
        SoundManagerScript.UpdateAudioMixerGroupVolume(audioMixer, soundEffectsVolume);
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

    public void UpdateOptionVolume(Slider slider)
    {
        soundEffectsVolume = slider.value;
    }

    public void UpdateOptionIsFullScreen(Toggle toggle)
    {
        isFullScreen = toggle.isOn;
    }
}