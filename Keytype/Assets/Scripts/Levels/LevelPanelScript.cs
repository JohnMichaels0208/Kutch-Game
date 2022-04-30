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
        instantiatedLevelButton = Instantiate(levelButtonToInstantiate, transform);
        LevelData levelData = new LevelData(levelName, levelDescription, instantiatedLevelButton, levelSceneName, levelPointsForOneStar);
        LevelButtonScript instantiatedLevelButtonScript = instantiatedLevelButton.GetComponent<LevelButtonScript>();
        instantiatedLevelButtonScript.SyncLevelDataToButton(levelData);
        instantiatedLevelButtonScript.levelDataDisplayer = levelDataDisplayerGO;
        SaveSystemScript.AddLevelData(levelData);
    }
}