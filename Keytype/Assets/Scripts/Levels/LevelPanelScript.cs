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
    [TextArea] public string levelDescription;

    private void Awake()
    {
        int mostRecentUnlockedLevelIndex = 0;

        levelDatas = SaveSystemScript.LoadLevelDataList();

        for (int i = 0; i < levelDatas.Count; i++)
        {
            if (i == 0)
            {
                mostRecentUnlockedLevelIndex = i;
            }

            if (levelDatas[i].stars > 0)
            {
                mostRecentUnlockedLevelIndex = i;
                continue;
            }

            if (i > mostRecentUnlockedLevelIndex)
            {
                levelDatas[i].isUnlocked = false;
            }

            if (i == mostRecentUnlockedLevelIndex + 1)
            {
                levelDatas[i].isUnlocked = true;
            }
        }

        SaveSystemScript.SaveLevelDataList(levelDatas);
    }

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