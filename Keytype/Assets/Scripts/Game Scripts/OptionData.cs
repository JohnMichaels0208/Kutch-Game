using UnityEngine;
[System.Serializable]
public class OptionData
{
    public float soundEffectsVolume;
    public bool isFullScreen;

    public OptionData(Option option)
    {
        this.soundEffectsVolume = option.soundEffectsVolume;
        this.isFullScreen = option.isFullScreen;
    }
}
