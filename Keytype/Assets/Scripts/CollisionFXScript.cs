using UnityEngine;

public class CollisionFXScript : MonoBehaviour
{
    public void OnFXAnimationEnd()
    {
        Destroy(gameObject);
    }
}
