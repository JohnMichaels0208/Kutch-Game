using UnityEngine;
using TMPro;

public class FallingElementBase : MonoBehaviour
{
    protected AudioSource audioSourceComponent;
    private Animator animatorComponent;
    private TextMeshPro tmpComponent;
    [SerializeField] private GameObject textGO;
    private const string animatorExplodedParamName = "Exploded";
    private const string animatorCollidedParamName = "Collided";

    private float fallingSpeed;
    private KeyCode associatedKeyCode;

    private float accelerationAmount;


    protected virtual void Awake()
    {
        audioSourceComponent = GetComponent<AudioSource>();
        animatorComponent = GetComponent<Animator>();
        tmpComponent = textGO.transform.GetComponent<TextMeshPro>();
    }

    protected virtual void Start()
    {
        associatedKeyCode = GameManagerScript.instance.keyCodes[Random.Range(0, GameManagerScript.instance.keyCodes.Length)];
        GameManagerScript.instance.keyCodesOnScreeen[associatedKeyCode] = true;
        GameManagerScript.instance.keyDownEvent += OnKeyDown;
        GameManagerScript.instance.pauseGameEvent += OnDisableFallingElements;
        tmpComponent.text = associatedKeyCode.ToString();
        fallingSpeed = Random.Range(GameManagerScript.instance.averageFallspeed - GameManagerScript.instance.fallSpeedDifference / 2, GameManagerScript.instance.averageFallspeed + GameManagerScript.instance.fallSpeedDifference / 2);
    }

    protected virtual void Update()
    {
        accelerationAmount += Time.deltaTime * GameManagerScript.instance.accelerationSpeed;
        transform.Translate(Vector3.down * Time.deltaTime * Mathf.Lerp(0, fallingSpeed, accelerationAmount));
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Death Zone")
        {
            Collide();
        }
        if (collision.tag == "Explosion")
        {
            Blast();
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
            CorrectKey();
        }
    }

    protected virtual void CorrectKey()
    {
        GameManagerScript.instance.keyCodesOnScreeen[associatedKeyCode] = false;
        fallingSpeed = 0;
        GameManagerScript.instance.AddPoints();
        if (animatorComponent != null)
        {
            animatorComponent.SetBool(animatorExplodedParamName, true);
        }
    }

    protected virtual void Blast()
    {
        GameManagerScript.instance.keyCodesOnScreeen[associatedKeyCode] = false;
    }

    protected virtual void Collide()
    {
        GameManagerScript.instance.keyCodesOnScreeen[associatedKeyCode] = false;
        fallingSpeed = 0;
        if (animatorComponent != null)
        {
            animatorComponent.SetBool(animatorCollidedParamName, true);
        }
        GameManagerScript.instance.RemoveHealth(GameManagerScript.instance.collidedMistakeText);
    }

    public virtual void OnFXAnimationEnd()
    {
        Destroy(gameObject);
    }

    private void TogglePauseFallingElements()
    {
        if (this != null)
        {
            enabled = !enabled;
        }
    }
}
