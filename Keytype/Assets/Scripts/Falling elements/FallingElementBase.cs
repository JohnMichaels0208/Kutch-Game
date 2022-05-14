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

    private float fallingSpeed;
    private KeyCode associatedKeyCode;

    protected bool correctKeyStarted = false;
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
        GameManagerScript.instance.pauseGameEvent += OnPauseGame;
        GameManagerScript.instance.speedChangedEvent += OnSpeedChanged;
        tmpComponent.text = associatedKeyCode.ToString();
        fallingSpeed = Random.Range(GameManagerScript.instance.currentFallspeed - GameManagerScript.instance.fallSpeedDifference / 2, GameManagerScript.instance.currentFallspeed + GameManagerScript.instance.fallSpeedDifference / 2);
    }

    protected virtual void Update()
    {
        if (collided) return;
        transform.Translate(Vector3.down * Time.deltaTime * fallingSpeed);
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
        ToggleEnable();
    }

    private void OnPauseGame(object sender, System.EventArgs eventArgs)
    {
        ToggleEnable();
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
        fallingSpeed = 0;
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

    private void ToggleEnable()
    {
        if (this != null)
        {
            enabled = !enabled;
        }
    }

    private void OnSpeedChanged(object sender, System.EventArgs eventArgs)
    {
        fallingSpeed = GameManagerScript.instance.currentFallspeed;
    }
}
