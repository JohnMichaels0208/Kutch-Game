using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverElementsPanel : MonoBehaviour
{
    [SerializeField] private float delaySeconds;
    [SerializeField] private GameObject elementsPanel;
    public void ZGameOverAnimationEnd()
    {
        StartCoroutine(LoopSetActive());
    }

    IEnumerator LoopSetActive()
    {
        for (int i = 0; i < elementsPanel.transform.childCount; i++)
        {
            elementsPanel.transform.GetChild(i).gameObject.SetActive(true);
            yield return new WaitForSeconds(delaySeconds);
        }
    }
}
