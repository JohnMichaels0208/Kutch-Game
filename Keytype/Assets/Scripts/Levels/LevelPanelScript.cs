using UnityEngine;
using TMPro;

public class LevelPanelScript : MonoBehaviour
{
    private GameObject instantiatedLevelButton;
    [SerializeField] private GameObject levelDataDisplayerGO;
    [SerializeField] private GameObject levelButtonToInstantiate;
    [Header("Level to create data")]
    public string levelName;
    public string levelSceneName;
    public float levelPointsForOneStar;
    [TextArea] public string levelDescription;

    public void CreateNewLevel()
    {
        LevelData levelData = new LevelData(levelName, levelDescription, instantiatedLevelButton, levelSceneName, levelPointsForOneStar);
        instantiatedLevelButton = Instantiate(levelButtonToInstantiate, transform);
        SyncLevelDataWithUI(levelData, instantiatedLevelButton);
        SaveSystemScript.AddLevelData(levelData);
    }

    public void SyncLevelDataWithUI(LevelData data, GameObject levelButtonGO)
    {
        //setting game object name
        levelButtonGO.name = levelName;
        //setting ui text name
        levelButtonGO.transform.GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>().text = data.levelName;
        //setting properties
        LevelButtonScript levelButtonScriptComponent = levelButtonGO.GetComponent<LevelButtonScript>();
        levelButtonScriptComponent.levelDataDisplayer = levelDataDisplayerGO;
        levelButtonScriptComponent.associatedLevelData = data;
        levelButtonScriptComponent.SyncLevelToSaveProperties(data);
    }
}