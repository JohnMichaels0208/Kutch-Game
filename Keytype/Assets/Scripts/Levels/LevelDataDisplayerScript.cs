using UnityEngine;
using TMPro;

public class LevelDataDisplayerScript : MonoBehaviour
{
    [SerializeField] private GameObject levelDataTitleGO;
    private TextMeshProUGUI levelDataTitleTMP;

    private string currentActiveLevelName;
    private void OnEnable()
    {
        levelDataTitleTMP = levelDataTitleGO.GetComponent<TextMeshProUGUI>();
    }

    public void SetCurrentActiveLevel()
    {

    }
}
