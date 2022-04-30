using UnityEngine;

public class DeathZoneScript : MonoBehaviour
{
    private float width;
    private void Awake()
    {
        width = Camera.main.orthographicSize * 2 * Screen.width / Screen.height;
    }
    void Start()
    {
        transform.localScale = new Vector3(width, 1, 1);
    }
}
