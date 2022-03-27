using UnityEngine;
using TMPro;

public class LevelDataDisplayerScript : MonoBehaviour
{
    [SerializeField] private GameObject levelDataTitleGO;
    [SerializeField] private GameObject levelDataDescriptionGO;
    [SerializeField] private GameObject playButton;
    private TextMeshProUGUI levelDataTitleTMP;
    private TextMeshProUGUI levelDataDescriptionTMP;

    private LevelData currentActiveLevelData;

    public void UpdateCurrentActiveLevel(LevelData levelData)
    {
        currentActiveLevelData = levelData;

        levelDataDescriptionTMP = levelDataDescriptionGO.GetComponent<TextMeshProUGUI>();
        levelDataDescriptionTMP.text = currentActiveLevelData.levelDescription;

        levelDataTitleTMP = levelDataTitleGO.GetComponent<TextMeshProUGUI>();
        levelDataTitleTMP.text = currentActiveLevelData.levelName;
        
        playButton.GetComponent<ButtonLoadLevelScript>().targetSceneName = currentActiveLevelData.levelSceneName;
    }
}
