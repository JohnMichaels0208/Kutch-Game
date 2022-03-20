using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.Audio;

public class Option : MonoBehaviour
{
    public float soundEffectsVolume = 0;
    public bool isFullScreen = true;
    [SerializeField] private AudioMixer audioMixer;

    public void SaveOption()
    {
        SoundManagerScript.UpdateAudioMixerGroupVolume(audioMixer);
        Screen.fullScreen = isFullScreen;
        SaveSystemScript.SaveOption(this);
    }

    public void LoadOption()
    {
        OptionData optionData = SaveSystemScript.LoadOption();
        this.soundEffectsVolume = optionData.soundEffectsVolume;
    }

    private void OnEnable()
    {
        if (!File.Exists(Application.persistentDataPath + "/savefile.json"))
        {
            SaveOption();
        }
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