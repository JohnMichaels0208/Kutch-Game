using UnityEngine;
using TMPro;

public class FallingElementScript : MonoBehaviour
{
    [SerializeField] private GameObject             explodeFX;
    [SerializeField] private GameObject             collisionFX;
    private TextMeshPro                             textMeshProUGUI;
    [SerializeField] private GameObject             textGameObject;

    private float                                   fallingSpeed = 2f;
    public GameCharacter                            associatedGameCharacter;

    public delegate void FallingElementCollideEventHandler(object sender, System.EventArgs eventArgs);
    public event FallingElementCollideEventHandler fallingElementCollideEvent;

    void Start()
    {
        GameManagerScript.instance.pauseGameEvent += OnDisableFallingElements;
        textMeshProUGUI = textGameObject.transform.GetComponent<TextMeshPro>();
        textMeshProUGUI.text = associatedGameCharacter.label.ToString();
        SetRandomFallSpeed(GameManagerScript.instance.averageFallspeed - GameManagerScript.instance.fallSpeedDifference / 2, GameManagerScript.instance.averageFallspeed + GameManagerScript.instance.fallSpeedDifference / 2);
    }

    void Update()
    {
        if (GameManagerScript.instance.currentKeycodeDetected == associatedGameCharacter.keyCode)
        {
            Explode();
        }
        transform.Translate(Vector3.down * Time.deltaTime * fallingSpeed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Death Zone")
        {
            Collide();
        }
    }
        
    private void OnEndGame(object sender, System.EventArgs eventArgs)
    {
        TogglePauseFallingElements();
    }

    private void OnDisableFallingElements(object sender, System.EventArgs eventArgs)
    {
        TogglePauseFallingElements();
    }

    public void Explode()
    {
        GameManagerScript.instance.AddPoints();
        SoundManagerScript.PlaySound(GameManagerScript.instance.audioSource, GameManagerScript.instance.explodeSound);
        if (explodeFX != null)
        {
            explodeFX.SetActive(true);
        }
    }

    private void Collide()
    {
        SoundManagerScript.PlaySound(GameManagerScript.instance.audioSource, GameManagerScript.instance.collideSound);
        OnFallingElementCollide();
        collisionFX.SetActive(true);
        collisionFX.transform.parent = null;
        Destroy(gameObject);
    }

    private void TogglePauseFallingElements()
    {
        if (this != null)
        {
            enabled = !enabled;
        }
    }

    protected virtual void OnFallingElementCollide()
    {
        if (fallingElementCollideEvent != null)
        {
            fallingElementCollideEvent.Invoke(this, System.EventArgs.Empty);
        }
    }

    private void SetRandomFallSpeed(float minFallSpeed, float maxFallSpeed)
    {
        fallingSpeed = Random.Range(minFallSpeed, maxFallSpeed);
    }
}
