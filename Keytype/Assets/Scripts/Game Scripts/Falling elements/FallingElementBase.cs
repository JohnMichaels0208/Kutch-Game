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

    private float currentFallingSpeed;
    private KeyCode associatedKeyCode;

    private bool correctKeyStarted = false;
    private bool collided = false;


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
        GameManagerScript.instance.speedChangedEvent += OnSpeedChanged;
        tmpComponent.text = associatedKeyCode.ToString();
        currentFallingSpeed = Random.Range(GameManagerScript.instance.currentFallspeed - GameManagerScript.instance.fallSpeedDifference / 2, GameManagerScript.instance.currentFallspeed + GameManagerScript.instance.fallSpeedDifference / 2);
    }

    protected virtual void Update()
    {
        if (collided) return;
        transform.Translate(Vector3.down * Time.deltaTime * currentFallingSpeed);
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
        if (correctKeyStarted) return;
        SoundManagerScript.PlaySound(audioSourceComponent, correctKeySound);
        GameManagerScript.instance.keyCodesOnScreeen[associatedKeyCode] = false;
        currentFallingSpeed = 0;
        GameManagerScript.instance.AddPoints();
        if (animatorComponent != null)
        {
            animatorComponent.SetBool(animatorExplodedParamName, true);
        }
        correctKeyStarted = true;
    }

    protected virtual void TouchedByBombBlast()
    {
        CorrectKey();
    }

    protected virtual void Collide()
    {
        collided = true;
        SoundManagerScript.PlaySound(audioSourceComponent, GameManagerScript.instance.collideSound);
        GameManagerScript.instance.keyCodesOnScreeen[associatedKeyCode] = false;
        currentFallingSpeed = 0;
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

    private void OnSpeedChanged(object sender, System.EventArgs eventArgs)
    {
        Debug.Log("On speed changed");
        currentFallingSpeed = GameManagerScript.instance.currentFallspeed;
    }
}
