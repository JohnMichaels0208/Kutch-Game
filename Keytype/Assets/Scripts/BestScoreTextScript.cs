using UnityEngine;
using TMPro;

public class BestScoreTextScript : MonoBehaviour
{
    private void OnEnable()
    {
        GetComponent<TextMeshProUGUI>().text = "Best Score: " + SaveSystemScript.LoadProgress().bestScore.ToString();
    }
}
