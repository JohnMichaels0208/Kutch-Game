using UnityEngine;
using TMPro;

public class LevelButtonScript : MonoBehaviour
{
    [SerializeField] private GameObject unlockedButton, lockedButton;
    [SerializeField] private GameObject levelNameGO;
    [SerializeField] private GameObject[] levelStarsGO;
    [HideInInspector] public LevelData associatedLevelData;
    public GameObject levelDataDisplayer;
    [Header("Level to save data")]
    public string levelToSaveName;
    public string levelToSaveSceneName;
    public float levelToSavePointsForOneStar;
    public KeyCode[] levelToSaveKeyCodes;
    [TextArea] public string levelToSaveDescription;
    private void OnEnable()
    {
        GetComponent<ButtonLoadLevelScript>().targetSceneName = associatedLevelData.associatedSceneName;
    }

    private void Start()
    {
        associatedLevelData = SaveSystemScript.LoadLevelDataList()[SaveSystemScript.LoadLevelIndexByButtonName(gameObject.name)];
        SyncLevelDataToButton(associatedLevelData);
        switch (associatedLevelData.isUnlocked)
        {
            case false:
                lockedButton.SetActive(true);
                break;

            case true:
                unlockedButton.SetActive(true);
                break;
        }


    }

    public void SetCurrentActiveLevelData()
    {
        levelDataDisplayer.GetComponent<LevelDataDisplayerScript>().UpdateLevelDataDisplayText(associatedLevelData);
    }

    public void SaveLevel()
    {
        LevelData levelData = new LevelData(levelToSaveName, levelToSaveDescription, gameObject.name, levelToSaveSceneName, levelToSavePointsForOneStar, levelToSaveKeyCodes);
        SaveSystemScript.SaveLevelData(SaveSystemScript.LoadLevelIndexByButtonName(gameObject.name), levelData);
    }

    public void DeleteLevel()
    {
        SaveSystemScript.DeleteLevelData(SaveSystemScript.LoadLevelIndexByButtonName(gameObject.name));
        DestroyImmediate(gameObject);
    }

    public void SyncDataWithLevelToSaveProperties(LevelData data)
    {
        levelToSaveName = data.levelName;
        levelToSaveSceneName = data.associatedSceneName;
        levelToSavePointsForOneStar = data.levelPointsForOneStar;
        levelToSaveDescription = data.levelDescription;
        levelToSaveKeyCodes = data.keyCodes;
    }

    public void SyncLevelDataToButton(LevelData data)
    {
        //setting game object name
        gameObject.name = data.levelName;
        //setting ui text name
        levelNameGO.GetComponent<TextMeshProUGUI>().text = data.levelName;
        //setting ui stars
        int starsTakenIntoCount = data.stars;
        for (int i = 0; i < levelStarsGO.Length; i++)
        {
            if (starsTakenIntoCount > 0)
            {
                levelStarsGO[i].SetActive(true);
                starsTakenIntoCount--;
                continue;
            }
            else if (starsTakenIntoCount <= 0)
            {
                levelStarsGO[i].SetActive(false);
                continue;
            }
        }
        //setting properties
        associatedLevelData = data;
        SyncDataWithLevelToSaveProperties(data);
    }
}
