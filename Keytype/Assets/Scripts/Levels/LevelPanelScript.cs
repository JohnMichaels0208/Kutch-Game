using UnityEngine;
using TMPro;

public class LevelPanelScript : MonoBehaviour
{
    [SerializeField] private GameObject levelDataDisplayerGO;
    private GameObject instantiatedLevelButton;
    [SerializeField] private GameObject levelButton;
    public string levelName;
    public string levelSceneName;
    [TextArea] public string levelDescription;

    public void CreateNewLevel()
    {
        instantiatedLevelButton = Instantiate(levelButton, transform);
        LevelData levelData = new LevelData(levelName, levelDescription, instantiatedLevelButton, levelSceneName);
        instantiatedLevelButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = levelData.levelName;
        instantiatedLevelButton.GetComponent<LevelButtonScript>().levelDataDisplayer = levelDataDisplayerGO;
        instantiatedLevelButton.GetComponent<LevelButtonScript>().associtatedLevelData = levelData;
        SaveSystemScript.SaveLevel(levelData);
    }

    public void DeleteLevelByName()
    {
        DestroyImmediate(SaveSystemScript.LoadLevelByName(levelName).associatedButton);
        SaveSystemScript.DeleteLevel(SaveSystemScript.LoadLevelByName(levelName));
    }
}
