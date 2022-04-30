using UnityEngine;
using TMPro;

public abstract class FallingElementBase : MonoBehaviour
{
    protected abstract Sound correctKeySound { get; }
    protected AudioSource audioSourceComponent;
    private Animator animatorComponent;
    private TextMeshPro tmpComponent;
    [SerializeField] private GameObject textGO;
    private const string animatorExplodedParamName = "Exploded";
    private const string animatorCollidedParamName = "Collided";

    private float targetFallingSpeed;
    private float currentFallingSpeed;
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
        currentFallingSpeed = 0;
        associatedKeyCode = GameManagerScript.instance.keyCodes[Random.Range(0, GameManagerScript.instance.keyCodes.Length)];
        GameManagerScript.instance.keyCodesOnScreeen[associatedKeyCode] = true;
        GameManagerScript.instance.keyDownEvent += OnKeyDown;
        GameManagerScript.instance.pauseGameEvent += OnDisableFallingElements;
        GameManagerScript.instance.timerEvent += OnTimer;
        tmpComponent.text = associatedKeyCode.ToString();
        targetFallingSpeed = Random.Range(GameManagerScript.instance.currentFallspeed - GameManagerScript.instance.fallSpeedDifference / 2, GameManagerScript.instance.currentFallspeed + GameManagerScript.instance.fallSpeedDifference / 2);
    }

    protected virtual void Update()
    {
        accelerationAmount += Time.deltaTime * GameManagerScript.instance.accelerationSpeed;
        currentFallingSpeed = Time.deltaTime * Mathf.Lerp(currentFallingSpeed, targetFallingSpeed, accelerationAmount);
        transform.Translate(Vector3.down * currentFallingSpeed);
        GameManagerScript.instance.keyCodesOnScreeen[associatedKeyCode] = true;
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Death Zone")
        {
            Collide();
        }
        if (collision.tag == "Explosion")
        {
            TouchedByBombBlast();
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
        SoundManagerScript.PlaySound(audioSourceComponent, correctKeySound);
        GameManagerScript.instance.keyCodesOnScreeen[associatedKeyCode] = false;
        targetFallingSpeed = 0;
        GameManagerScript.instance.AddPoints();
        if (animatorComponent != null)
        {
            animatorComponent.SetBool(animatorExplodedParamName, true);
        }
    }

    protected virtual void TouchedByBombBlast()
    {
        CorrectKey();
    }

    protected virtual void Collide()
    {
        SoundManagerScript.PlaySound(audioSourceComponent, GameManagerScript.instance.collideSound);
        GameManagerScript.instance.keyCodesOnScreeen[associatedKeyCode] = false;
        targetFallingSpeed = 0;
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

    private void OnTimer(object sender, System.EventArgs eventArgs)
    {
        targetFallingSpeed = GameManagerScript.instance.timerFallSpeed;
    }
}
