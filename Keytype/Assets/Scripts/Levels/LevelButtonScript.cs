using UnityEngine;

public class LevelButtonScript : MonoBehaviour
{
    [HideInInspector] public LevelData associatedLevelData;
    public GameObject levelDataDisplayer;
    [Header("Level to save data")]
    public string levelToSaveName;
    public string levelToSaveSceneName;
    public float levelToSavePointsForOneStar;
    [TextArea] public string levelToSaveDescription;

    private void OnEnable()
    {
        GetComponent<ButtonLoadLevelScript>().targetSceneName = associatedLevelData.levelSceneName;
    }

    public void SetCurrentActiveLevelData()
    {
        levelDataDisplayer.GetComponent<LevelDataDisplayerScript>().UpdateCurrentActiveLevel(associatedLevelData);
    }

    public void SaveLevel()
    {
        LevelData levelData = new LevelData(levelToSaveName, levelToSaveDescription, gameObject, levelToSaveSceneName, levelToSavePointsForOneStar);
        SaveSystemScript.SaveLevelData(SaveSystemScript.LoadLevelIndexByGO(gameObject), levelData);
    }

    public void DeleteLevel()
    {
        SaveSystemScript.DeleteLevelData(SaveSystemScript.LoadLevelIndexByGO(gameObject));
        DestroyImmediate(gameObject);
    }

    public void SyncLevelToSaveProperties(LevelData data)
    {
        levelToSaveName = data.levelName;
        levelToSaveSceneName = data.levelSceneName;
        levelToSavePointsForOneStar = data.levelPointsForOneStar;
        levelToSaveDescription = data.levelDescription;
    }
}
