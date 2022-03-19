using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class GameManagerScript : MonoBehaviour
{
    [SerializeField] private Sound explodeSound;
    [SerializeField] private Sound collideSound;
    [SerializeField] private Sound gameOverSound;

    //Reference to parent of ui elements
    [SerializeField] private Transform heartsUIParent;

    [SerializeField] private GameObject heartUI;

    [SerializeField] private GameObject pauseUI;

    [SerializeField] private GameObject gameOverUI;

    [SerializeField] private GameObject OptionsUI;

    private AudioSource audioSource;

    // Reference to the object to spawn
    [SerializeField] private Transform fallingElement;

    //Spawn Position Varibles
    private const float spawnYPosition = 6;
    private const float minSpawnXPosition = 7.8f;
    private const float maxSpawnXPosition = -7.8f;

    //Fall speed of the falling elements
    private float averageFallspeed = 2;
    private float fallSpeedDifference = 1f;

    //Countdown Variables
    private float countdownAmt = 0.5f;
    private float countdownElapsed;
    private int spawnAmtPerCountdown = 1;
    private bool countdownComplete;

    //List containing all game characters
    public List<GameCharacter> allGameCharacters;

    [HideInInspector] public KeyCode currentKeycodeDetected;

    [HideInInspector] public int lives = 1;

    private bool gameOver;
    private bool gamePaused = false;

    public delegate void PauseGameEventHandler(object sender, System.EventArgs eventArgs);
    public event PauseGameEventHandler pauseGameEvent;

    private List<FallingElementScript> fallingElementsOnScreen;

    private SoundManagerScript soundManagerScript;
    private void Start()
    {
        soundManagerScript = GetComponent<SoundManagerScript>();
        fallingElementsOnScreen = new List<FallingElementScript>();
        audioSource = GetComponent<AudioSource>();
        countdownElapsed = countdownAmt;
        for (int i = 1; i < lives; i++)
        {
            GameObject.Instantiate(heartUI, heartsUIParent);
        }

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePauseGame();
        }
        if (!gameOver && !gamePaused)
        {
            currentKeycodeDetected = GetCurrentInputKeycode();
            countdownComplete = countdownElapsed <= 0;
            CountDown();
            if (countdownComplete)
            {
                SpawnLetter();
                ResetCountdownElapsed();
            }
            for (int i = 0; i < fallingElementsOnScreen.Count; i++)
            {
                if (fallingElementsOnScreen[i].associatedGameCharacter.keyCode == currentKeycodeDetected)
                {
                    fallingElementsOnScreen[i].Explode();
                    soundManagerScript.PlaySound(audioSource, explodeSound);
                    fallingElementsOnScreen.RemoveAt(i);
                }
            }

        }
    }

    private void SpawnLetter()
    {
        for (int i = 0; i < spawnAmtPerCountdown; i++)
        {
            FallingElementScript instantiatedOBjectFallingElementScript = GameObject.Instantiate(fallingElement, new Vector3(Random.Range(minSpawnXPosition, maxSpawnXPosition), spawnYPosition, 0), Quaternion.identity).GetComponent<FallingElementScript>();
            instantiatedOBjectFallingElementScript.SetRandomFallSpeed(averageFallspeed - fallSpeedDifference/2, averageFallspeed + fallSpeedDifference /2);
            instantiatedOBjectFallingElementScript.gameManagerScript = this;
            instantiatedOBjectFallingElementScript.associatedGameCharacter = allGameCharacters[Random.Range(0, allGameCharacters.Count)];
            instantiatedOBjectFallingElementScript.fallingElementCollideEvent += OnFallingElementCollide;
            fallingElementsOnScreen.Add(instantiatedOBjectFallingElementScript);
        }
    }

    private float CountDown()
    {
        this.countdownElapsed -= Time.deltaTime;
        return countdownElapsed;
    }

    private void ResetCountdownElapsed()
    {
        this.countdownElapsed = this.countdownAmt;
    }

    private KeyCode GetCurrentInputKeycode()
    {
        KeyCode keyCode = KeyCode.None;
        foreach (KeyCode kcode in System.Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKeyDown(kcode))
            {
                return keyCode = kcode;
            }
        }
        return keyCode;
    }

    public void RemoveHealth()
    {
        lives --;
        RemoveHeartUI();
        if (lives <= 0)
        {
            EndGame();
            lives = 0;
        }
    }

    private void RemoveHeartUI()
    {
        if (heartsUIParent.GetChild(0).gameObject != null)
        {
            Destroy(heartsUIParent.GetChild(0).gameObject);
        }
    }

    private void EndGame()
    {
        soundManagerScript.PlaySound(audioSource, gameOverSound);
        gameOver = true;
        gameOverUI.SetActive(true);
        DisableFallingElements();
    }

    protected virtual void OnDisableFallingElements()
    {
        if (pauseGameEvent != null)
        {
            pauseGameEvent.Invoke(this, System.EventArgs.Empty);
        }
    }

    public void DisableFallingElements()
    {
        OnDisableFallingElements();
    }

    public void TogglePauseGame()
    {
        pauseUI.SetActive(!pauseUI.activeSelf);
        DisableFallingElements();
        gamePaused = !gamePaused;
    }

    private void OnFallingElementCollide(object sender, System.EventArgs eventArgs)
    {
        soundManagerScript.PlaySound(audioSource, collideSound);
        RemoveHealth();
    }

    public void ToggleOptions()
    {
        OptionsUI.SetActive(!OptionsUI.activeSelf);
    }
}
