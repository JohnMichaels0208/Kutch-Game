using UnityEngine;
using System.Linq;

[RequireComponent(typeof(GameEndedElementsPanel))]
public class StarsDisplayerScript : MonoBehaviour
{
    [SerializeField] private Transform starsPanel;
    private GameEndedElementsPanel elementsPanel;
    private void Awake()
    {
        elementsPanel = GetComponent<GameEndedElementsPanel>();
    }

    private void OnEnable()
    {
        for (int i = 0; i < GameManagerScript.instance.starsOfGame; i++)
        {
            elementsPanel.elements.Insert(0, starsPanel.GetChild(i).gameObject);
        }
    }
}
