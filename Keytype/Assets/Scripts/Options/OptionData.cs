[System.Serializable]
public class OptionData
{
    public float gameSoundVolume;
    public float soundEffectsVolume;
    public bool isFullScreen;

    public OptionData(float soundEffectsVolume, float gameSoundVolume, bool isFullScreen)
    {
        this.gameSoundVolume = gameSoundVolume;
        this.soundEffectsVolume = soundEffectsVolume;
        this.isFullScreen = isFullScreen;
    }
}
