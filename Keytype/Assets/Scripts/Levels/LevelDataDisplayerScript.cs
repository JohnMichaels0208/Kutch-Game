using UnityEngine;
using TMPro;

public class LevelDataDisplayerScript : MonoBehaviour
{
    [SerializeField] private GameObject levelDataTitleGO;
    [SerializeField] private GameObject levelDataDescriptionGO;
    [SerializeField] private GameObject playButton;
    private TextMeshProUGUI levelDataTitleTMP;
    private TextMeshProUGUI levelDataDescriptionTMP;


    public void UpdateLevelDataDisplayText(LevelData levelData)
    {
        Debug.Log(levelData.levelName);
        levelDataDescriptionTMP = levelDataDescriptionGO.GetComponent<TextMeshProUGUI>();
        levelDataDescriptionTMP.text = levelData.levelDescription + "\n" + "Stars: " + levelData.stars.ToString() + "\n" + "Best points:" + levelData.bestPoints.ToString();

        levelDataTitleTMP = levelDataTitleGO.GetComponent<TextMeshProUGUI>();
        levelDataTitleTMP.text = levelData.levelName;
        
        playButton.GetComponent<ButtonLoadLevelScript>().targetSceneName = levelData.levelSceneName;
    }
}
