using UnityEngine;
using System.Collections.Generic;
public class LevelPanelScript : MonoBehaviour
{
    private List<LevelData> levelDatas;

    [SerializeField] private GameObject levelDataDisplayerGO;
    [SerializeField] private GameObject levelButtonToInstantiate;
    [Header("Level to create data")]
    public string levelName;
    public string levelSceneName;
    public float levelPointsForOneStar;
    public KeyCode[] keyCodes;
    [TextArea] public string levelDescription;

    private void Awake()
    {
        int mostRecentUnlockedLevelIndex = 0;

        levelDatas = SaveSystemScript.LoadLevelDataList();

        for (int i = 0; i < levelDatas.Count; i++)
        {
            if (i == 0 || levelDatas[i].stars > 0)
            {
                levelDatas[i].isUnlocked = true;
                mostRecentUnlockedLevelIndex = i;
                continue;
            }

            if (i == mostRecentUnlockedLevelIndex + 1)
            {
                levelDatas[i].isUnlocked = true;
                continue;
            }

            if (i > mostRecentUnlockedLevelIndex)
            {
                Debug.Log(i + " false");
                levelDatas[i].isUnlocked = false;
                continue;
            }
        }

        SaveSystemScript.SaveLevelDataList(levelDatas);
    }

    public void CreateNewLevel()
    {
        GameObject instantiatedLevelButton = Instantiate(levelButtonToInstantiate, transform);
        LevelButtonScript instantiatedLevelButtonScript = instantiatedLevelButton.GetComponent<LevelButtonScript>();
        LevelData levelData = new LevelData(levelName, levelDescription, instantiatedLevelButton.name, levelSceneName, levelPointsForOneStar, keyCodes);
        instantiatedLevelButtonScript.SyncLevelDataToButton(levelData);
        levelData.associatedButtonName = instantiatedLevelButton.name;
        instantiatedLevelButtonScript.levelDataDisplayer = levelDataDisplayerGO;
        SaveSystemScript.AddLevelData(levelData);
    }
}