using UnityEngine;
using TMPro;

public class FallingElementScript : MonoBehaviour
{
    [SerializeField] private GameObject             explodeFX;
    [SerializeField] private GameObject             collisionFX;
    private TextMeshPro                             textMeshProUGUI;
    [SerializeField] private GameObject             textGameObject;

    private float                                   fallingSpeed;
    private GameCharacter                           associatedGameCharacter;

    private float accelerationAmount;

    void Start()
    {
        SetRandomAssociatedGameCharacter();
        GameManagerScript.instance.pauseGameEvent += OnDisableFallingElements;
        textMeshProUGUI = textGameObject.transform.GetComponent<TextMeshPro>();
        textMeshProUGUI.text = associatedGameCharacter.label.ToString();
        SetRandomFallSpeed(GameManagerScript.instance.averageFallspeed - GameManagerScript.instance.fallSpeedDifference / 2, GameManagerScript.instance.averageFallspeed + GameManagerScript.instance.fallSpeedDifference / 2);
    }

    void Update()
    {
        if (GameManagerScript.instance.currentKeycodeDetected == associatedGameCharacter.keyCode && GameManagerScript.instance.currentKeycodeDetected != KeyCode.None)
        {
            Explode();
        }
        accelerationAmount += Time.deltaTime * GameManagerScript.instance.accelerationSpeed;
        transform.Translate(Vector3.down * Time.deltaTime * Mathf.Lerp(0, fallingSpeed, accelerationAmount));
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
        GameManagerScript.instance.RemoveHealth();
        SoundManagerScript.PlaySound(GameManagerScript.instance.audioSource, GameManagerScript.instance.collideSound);
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

    private void SetRandomAssociatedGameCharacter()
    {
        associatedGameCharacter = GameManagerScript.instance.gameCharacters[Random.Range(0, GameManagerScript.instance.gameCharacters.Count)];
    }

    private void SetRandomFallSpeed(float minFallSpeed, float maxFallSpeed)
    {
        fallingSpeed = Random.Range(minFallSpeed, maxFallSpeed);
    }
}