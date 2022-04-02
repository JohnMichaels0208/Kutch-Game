using UnityEngine;
using TMPro;

public class EndGamePointsScript : MonoBehaviour
{
    private TextMeshProUGUI tmpComponent;
    private float currentPointDisplayValue = 0;
    private float lerpSpeed = 1;

    private void Awake()
    {
        tmpComponent = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        if (currentPointDisplayValue < GameManagerScript.instance.pointsOfGame)
        {
            currentPointDisplayValue += Time.deltaTime * lerpSpeed * GameManagerScript.instance.pointsOfGame;
            tmpComponent.text = "Points: " + Mathf.RoundToInt(currentPointDisplayValue);
        }
        else if (currentPointDisplayValue > GameManagerScript.instance.pointsOfGame)
        {
            tmpComponent.text = "Points: " + GameManagerScript.instance.pointsOfGame;
        }
    }
}
