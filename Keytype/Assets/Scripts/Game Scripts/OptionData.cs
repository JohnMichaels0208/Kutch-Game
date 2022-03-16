[System.Serializable]
public class OptionData
{
    public float volume;

    public OptionData(Option options)
    {
        this.volume = options.volume;
    }
}
