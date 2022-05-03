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
        levelDataDescriptionTMP = levelDataDescriptionGO.GetComponent<TextMeshProUGUI>();
        levelDataDescriptionTMP.text =
            levelData.levelDescription +
            "\nOne star points: "   + levelData.levelPointsForOneStar * new OneStarCondition().pointsCoefficientForStar +
            "\nTwo star points: "   + levelData.levelPointsForOneStar * new TwoStarCondition().pointsCoefficientForStar + 
            "\nThree star points: " + levelData.levelPointsForOneStar * new ThreeStarCondition().pointsCoefficientForStar +
            "\nStars: "             + levelData.stars + 
            "\nBest points:"        + levelData.bestPoints;

        levelDataTitleTMP = levelDataTitleGO.GetComponent<TextMeshProUGUI>();
        levelDataTitleTMP.text = levelData.levelName;
        
        playButton.GetComponent<ButtonLoadLevelScript>().targetSceneName = levelData.associatedSceneName;
    }
}
