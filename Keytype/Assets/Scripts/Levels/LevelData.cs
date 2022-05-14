[System.Serializable]
public class LevelData
{
    public bool isUnlocked;
    public string levelName;
    public string levelDescription;

    public int stars;
    public float bestPoints;

    public string associatedButtonName;
    public string associatedSceneName;

    public float levelPointsForOneStar;
    public UnityEngine.KeyCode[] keyCodes;
    

    public LevelData(string name, string description, string associatedButtonName, string sceneName, float pointsForOneStar, UnityEngine.KeyCode[] keyCodes)
    {
        levelName = name;
        levelDescription = description;
        isUnlocked = true;

        
        bestPoints = 0;
        stars = 0;

        this.associatedButtonName = associatedButtonName;
        associatedSceneName = sceneName;

        levelPointsForOneStar = pointsForOneStar;
        this.keyCodes = keyCodes;
    }
}
