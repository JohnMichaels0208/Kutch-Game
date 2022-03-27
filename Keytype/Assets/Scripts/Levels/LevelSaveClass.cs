using System.Collections.Generic;
using System;

[Serializable]
public class LevelSaveClass
{
    public List<LevelData> listOfLevels;

    public LevelSaveClass(List<LevelData> levelsList)
    {
        this.listOfLevels = levelsList;
    }
}
