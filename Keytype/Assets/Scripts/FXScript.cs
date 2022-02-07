using UnityEngine;

public class FXScript : MonoBehaviour
{
    public void OnFXAnimationEnd()
    {
        Destroy(gameObject);
    }
}
