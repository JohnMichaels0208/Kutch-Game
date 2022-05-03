[System.Serializable]
public class LevelData
{
    public bool isUnlocked;
    public string levelName;
    public string levelDescription;
    public int stars;
    public string associatedButtonName;
    public string associatedSceneName;
    public float levelPointsForOneStar;
    public float bestPoints;

    public LevelData(string name, string description, string associatedButtonName, string sceneName, float pointsForOneStar)
    {
        isUnlocked = true;
        levelPointsForOneStar = pointsForOneStar;
        bestPoints = 0;
        stars = 0;
        this.associatedButtonName = associatedButtonName;
        levelName = name;
        levelDescription = description;
        associatedSceneName = sceneName;
    }
}
