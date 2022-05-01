using System;
using UnityEngine;

[Serializable]
public class LevelData
{
    public string levelName;
    public string levelDescription;
    public string levelSceneName;
    public int stars;
    public string associatedButtonName;
    public float levelPointsForOneStar;
    public float bestPoints;

    public LevelData(string name, string description, string associatedButtonName, string sceneName, float pointsForOneStar)
    {
        this.levelPointsForOneStar = pointsForOneStar;
        bestPoints = 0;
        stars = 0;
        this.associatedButtonName = associatedButtonName;
        levelName = name;
        levelDescription = description;
        levelSceneName = sceneName;
    }
}
