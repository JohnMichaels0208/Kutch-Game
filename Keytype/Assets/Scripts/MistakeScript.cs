using UnityEngine;

public class MistakeScript : MonoBehaviour
{
    public void AnimationEnd()
    {
        gameObject.SetActive(false);
    }
}
