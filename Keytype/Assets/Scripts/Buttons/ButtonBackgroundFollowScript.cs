using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class ButtonBackgroundFollowScript : MonoBehaviour
{
    private float followSpeed = 30;
    private RectTransform rectTransform;
    private Vector3 targetPosition = new Vector3(-386, 25, 0);
    private Image background;

    private void Awake()
    {
        background = GetComponent<Image>();
        rectTransform = GetComponent<RectTransform>();
    }

    public void ButtonPointerEnter(RectTransform targetRectTransform)
    {
        targetPosition = targetRectTransform.position;
    }

    public void PointerEnter()
    {
        background.color = new Color(background.color.r, background.color.g, background.color.b, 1);
    }

    public void PointerExit()
    {
        background.color *= new Color(1, 1, 1, 0);
    }

    private void Update()
    {
            rectTransform.position = new Vector3
            (
                targetPosition.x, 
                Mathf.LerpUnclamped(rectTransform.position.y, targetPosition.y, Time.deltaTime * followSpeed), 
                0
            );
    }
}
