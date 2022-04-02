using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TitleAnimationScript : MonoBehaviour
{
    private RectTransform[] titleLetters;
    private float timePassed;
    private float waveSpeed = 5f;
    private float magnitude = 15f;
    private RectTransform thisRectTransform;
    [SerializeField] private Transform canvas;

    public char[] letters;
    [SerializeField] private RectTransform letterToInstantiate;

    public AnimationType animationStyle = AnimationType.wave;

    public enum AnimationType 
    {
        wave
    };

    private void Awake()
    {
        thisRectTransform = GetComponent<RectTransform>();
        titleLetters = new RectTransform[letters.Length];
        for (int i = 0; i < letters.Length; i++)
        {
            RectTransform instantiatedLetter = Instantiate(letterToInstantiate, thisRectTransform);
            instantiatedLetter.GetComponent<TextMeshProUGUI>().text = letters[i].ToString();
            instantiatedLetter.position += new Vector3(
                canvas.InverseTransformPoint(thisRectTransform.position).x - letters.Length * instantiatedLetter.rect.width * 0.5f + instantiatedLetter.rect.width * i, 
                0, 
                0
            );
            titleLetters[i] = instantiatedLetter;
        }
    }

    private void Update()
    {
        timePassed += Time.deltaTime;
        switch (animationStyle)
        {
            case AnimationType.wave:
                
                for (int i = 0; i < titleLetters.Length; i++)
                {
                    titleLetters[i].localPosition = new Vector3(
                        titleLetters[i].localPosition.x,
                        Mathf.Sin(titleLetters[i].localPosition.x + timePassed * waveSpeed) * magnitude,
                        titleLetters[i].localPosition.z
                    );
                }
                break;
        }
    }
}
