[System.Serializable]
public class OptionData
{
    public float volume;

    public OptionData(Option option)
    {
        this.volume = option.volume;
    }
}
