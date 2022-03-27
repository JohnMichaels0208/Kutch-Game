[System.Serializable]
public class OptionData
{
    public float soundEffectsVolume;
    public bool isFullScreen;

    public OptionData(float soundEffectsVolume, bool isFullScreen)
    {
        this.soundEffectsVolume = soundEffectsVolume;
        this.isFullScreen = isFullScreen;
    }
}
