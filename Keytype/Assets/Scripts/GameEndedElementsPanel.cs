using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEndedElementsPanel : MonoBehaviour
{
    [SerializeField] private float delaySeconds;
    public List<GameObject> elements;
    public void ZGameOverAnimationEnd()
    {
        StartCoroutine(LoopSetActive());
    }

    IEnumerator LoopSetActive()
    {
        for (int i = 0; i < elements.Count; i++)
        {
            elements[i].SetActive(true);
            yield return new WaitForSeconds(delaySeconds);
        }
    }
}
