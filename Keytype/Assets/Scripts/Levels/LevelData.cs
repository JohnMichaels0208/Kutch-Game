using System;
using UnityEngine;

[Serializable]
public class LevelData
{
    public string levelName;
    public string levelDescription;
    public string levelSceneName;
    public GameObject associatedButton;
    public LevelData(string levelName, string levelDescription, GameObject buttonGameObject, string sceneName)
    {
        this.associatedButton = buttonGameObject;
        this.levelName = levelName;
        this.levelDescription = levelDescription;
        this.levelSceneName = sceneName;
    }
}
