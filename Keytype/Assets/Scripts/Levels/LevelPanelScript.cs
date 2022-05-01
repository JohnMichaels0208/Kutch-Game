using UnityEngine;
public class LevelPanelScript : MonoBehaviour
{
    [SerializeField] private GameObject levelDataDisplayerGO;
    [SerializeField] private GameObject levelButtonToInstantiate;
    [Header("Level to create data")]
    public string levelName;
    public string levelSceneName;
    public float levelPointsForOneStar;
    [TextArea] public string levelDescription;

    public void CreateNewLevel()
    {
        GameObject instantiatedLevelButton = Instantiate(levelButtonToInstantiate, transform);
        LevelButtonScript instantiatedLevelButtonScript = instantiatedLevelButton.GetComponent<LevelButtonScript>();
        LevelData levelData = new LevelData(levelName, levelDescription, instantiatedLevelButton.name, levelSceneName, levelPointsForOneStar);
        instantiatedLevelButtonScript.SyncLevelDataToButton(levelData);
        levelData.associatedButtonName = instantiatedLevelButton.name;
        instantiatedLevelButtonScript.levelDataDisplayer = levelDataDisplayerGO;
        SaveSystemScript.AddLevelData(levelData);
    }
}