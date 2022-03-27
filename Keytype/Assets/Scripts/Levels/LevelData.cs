using System;

[Serializable]
public class LevelData
{
    public string levelName;
    public string levelDescription;
    public LevelData(string levelName, string levelDescription)
    {
        this.levelName = levelName;
        this.levelDescription = levelDescription;
    }
}
