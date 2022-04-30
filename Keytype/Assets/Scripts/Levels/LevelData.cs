using System;
using UnityEngine;

[Serializable]
public class LevelData
{
    public string levelName;
    public string levelDescription;
    public string levelSceneName;
    public GameObject associatedButton;
    public int stars;
    public float levelPointsForOneStar;
    public float bestPoints;

    public LevelData(string name, string description, GameObject buttonGameObject, string sceneName, float pointsForOneStar)
    {
        this.levelPointsForOneStar = pointsForOneStar;
        bestPoints = 0;
        stars = 0;
        associatedButton = buttonGameObject;
        levelName = name;
        levelDescription = description;
        levelSceneName = sceneName;
    }
}
