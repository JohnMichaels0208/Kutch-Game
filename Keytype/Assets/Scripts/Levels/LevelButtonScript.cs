using UnityEngine;
using TMPro;

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
        //associatedLevelData = SaveSystemScript.LoadLevelList()[SaveSystemScript.LoadLevelIndexByGO(gameObject)];
        SyncLevelDataToButton(associatedLevelData);
    }

    public void SetCurrentActiveLevelData()
    {
        levelDataDisplayer.GetComponent<LevelDataDisplayerScript>().UpdateLevelDataDisplayText(associatedLevelData);
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

    public void SyncDataWithLevelToSaveProperties(LevelData data)
    {
        levelToSaveName = data.levelName;
        levelToSaveSceneName = data.levelSceneName;
        levelToSavePointsForOneStar = data.levelPointsForOneStar;
        levelToSaveDescription = data.levelDescription;
    }

    public void SyncLevelDataToButton(LevelData data)
    {
        //setting game object name
        gameObject.name = data.levelName;
        //setting ui text name
        transform.GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>().text = data.levelName;
        //setting properties
        associatedLevelData = data;
        SyncDataWithLevelToSaveProperties(data);
    }
}
