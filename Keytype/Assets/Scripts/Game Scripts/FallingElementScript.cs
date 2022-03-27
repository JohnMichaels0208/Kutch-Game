using UnityEngine;
using TMPro;

public class FallingElementScript : MonoBehaviour
{
    [SerializeField] private GameObject             explodeFX;
    [SerializeField] private GameObject             collisionFX;
    private TextMeshPro                             textMeshProUGUI;
    [SerializeField] private GameObject             textGameObject;

    private float                                   fallingSpeed;
    private KeyCode                           associatedKeyCode;

    private float accelerationAmount;

    void Start()
    {
        SetRandomAssociatedGameCharacter();
        GameManagerScript.instance.keyDownEvent += OnKeyDown;
        GameManagerScript.instance.pauseGameEvent += OnDisableFallingElements;
        textMeshProUGUI = textGameObject.transform.GetComponent<TextMeshPro>();
        textMeshProUGUI.text = associatedKeyCode.ToString();
        SetRandomFallSpeed(GameManagerScript.instance.averageFallspeed - GameManagerScript.instance.fallSpeedDifference / 2, GameManagerScript.instance.averageFallspeed + GameManagerScript.instance.fallSpeedDifference / 2);
    }

    void Update()
    {
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

    private void OnKeyDown(object sender, System.EventArgs eventArgs, KeyCode currentKeyCodeDetected)
    {
        if (currentKeyCodeDetected == associatedKeyCode)
        {
            Explode();
        }
    }

    public void Explode()
    {
        fallingSpeed = 0;
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
        collisionFX.SetActive(true);
        collisionFX.transform.parent = null;
        GameManagerScript.instance.RemoveHealth();
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
        associatedKeyCode = GameManagerScript.instance.keyCodes[Random.Range(0, GameManagerScript.instance.keyCodes.Count)];
    }

    private void SetRandomFallSpeed(float minFallSpeed, float maxFallSpeed)
    {
        fallingSpeed = Random.Range(minFallSpeed, maxFallSpeed);
    }
}