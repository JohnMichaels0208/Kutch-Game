using UnityEngine;
using TMPro;

public class LevelButtonScript : MonoBehaviour
{
    public LevelData associtatedLevelData;
    public GameObject levelDataDisplayer;

    private void OnEnable()
    {
        GetComponent<ButtonLoadLevelScript>().targetSceneName = associtatedLevelData.levelSceneName;
    }

    public void SetCurrentActiveLevelData()
    {
        levelDataDisplayer.GetComponent<LevelDataDisplayerScript>().UpdateCurrentActiveLevel(associtatedLevelData);
    }
}
