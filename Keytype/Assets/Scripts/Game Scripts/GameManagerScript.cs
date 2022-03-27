using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using TMPro;

public class GameManagerScript : MonoBehaviour
{
    public static GameManagerScript instance;

    public Sound explodeSound;
    public Sound collideSound;

    [SerializeField] private Sound gameOverSound;

    //Reference to parent of ui elements
    [SerializeField] private Transform heartUIParent;

    [SerializeField] private GameObject heartUI;

    [SerializeField] private GameObject pauseUI;

    [SerializeField] private GameObject gameOverUI;

    [SerializeField] private GameObject optionsUI;

    [SerializeField] private GameObject pointsUI;

    [HideInInspector] public AudioSource audioSource;

    // Reference to the object to spawn
    [SerializeField] private Transform fallingElement;

    [SerializeField] private AudioMixer audioMixer;
    //Spawn Position Varibles
    private const float spawnYPosition = 5f;
    private const float minSpawnXPosition = 7.8f;
    private const float maxSpawnXPosition = -7.8f;

    //Fall speed of the falling elements
    public float averageFallspeed = 2;
    public float fallSpeedDifference = 1f;

    //Countdown Variables
    private float countdownAmt = 0.5f;
    private float timerElapsed;

    private float pointsPerLetterExplode = 10;

    //List containing all game characters
    public List<GameCharacter> gameCharacters;

    private KeyCode currentKeyCodeDetected;

    [HideInInspector] public int lives = 1;

    private bool gameOver;
    private bool gamePaused = false;

    //event when game is paused
    public delegate void PauseGameEventHandler(object sender, System.EventArgs eventArgs);
    public event PauseGameEventHandler pauseGameEvent;

    //event where a key is pressed down
    public delegate void KeyDownEventHandler(object sender, System.EventArgs eventArgs, KeyCode keyCodeDetected);
    public event KeyDownEventHandler keyDownEvent;

    //acceleration speed when falling element spawned
    public float accelerationSpeed = 2f;

    //game data
    private float pointsOfGame;
    [HideInInspector] public float pointsToSave;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        timerElapsed = countdownAmt;
        for (int i = 1; i < lives; i++)
        {
            GameObject.Instantiate(heartUI, heartUIParent);
        }

        SoundManagerScript.UpdateAudioMixerGroupVolume(audioMixer);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePauseGame();
        }
        if (!gameOver && !gamePaused)
        {
            currentKeyCodeDetected = GetCurrentInputKeycode();
            timerElapsed -= Time.deltaTime;
            if (timerElapsed <= 0)
            {
                SpawnLetter();
                timerElapsed = countdownAmt;
            }
            if (currentKeyCodeDetected != KeyCode.None)
            {
                OnKeyDown(currentKeyCodeDetected);
            }
        }
    }

    private void SpawnLetter()
    {
        GameObject.Instantiate(fallingElement, new Vector3(Random.Range(minSpawnXPosition, maxSpawnXPosition), spawnYPosition, 0), Quaternion.identity).GetComponent<FallingElementScript>();
    }
    private KeyCode GetCurrentInputKeycode()
    {
        KeyCode keyCode = KeyCode.None;
        foreach (KeyCode kcode in System.Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKeyDown(kcode))
            {
                Debug.Log(kcode);
                return keyCode = kcode;
            }
        }
        return keyCode;
    }
    public void RemoveHealth()
    {
        lives --;
        if (heartUIParent.GetChild(0).gameObject != null)
        {
            Destroy(heartUIParent.GetChild(0).gameObject);
        }
        if (lives <= 0)
        {
            EndGame();
            lives = 0;
        }
    }
    private void EndGame()
    {
        SoundManagerScript.PlaySound(audioSource, gameOverSound);
        gameOver = true;
        gameOverUI.SetActive(true);
        OnDisableFallingElements();
    }
    protected virtual void OnDisableFallingElements()
    {
        if (pauseGameEvent != null)
        {
            pauseGameEvent.Invoke(this, System.EventArgs.Empty);
        }
    }
    protected virtual void OnKeyDown(KeyCode keyCodeEventData)
    {
        if (keyDownEvent != null)
        {
            keyDownEvent.Invoke(this, System.EventArgs.Empty, keyCodeEventData);
        }
    }

    public void TogglePauseGame()
    {
        pauseUI.SetActive(!pauseUI.activeSelf);
        OnDisableFallingElements();
        gamePaused = !gamePaused;
    }
    public void ToggleOptions()
    {
        optionsUI.SetActive(!optionsUI.activeSelf);
    }
    public void AddPoints()
    {
        pointsOfGame += pointsPerLetterExplode;
        pointsUI.GetComponent<TextMeshProUGUI>().text = "Points: " + pointsOfGame;
    }
    public void SaveProgress()
    {
        if (SaveSystemScript.LoadProgress().bestScore <= pointsOfGame)
        {
            pointsToSave = pointsOfGame;
        }
        if (SaveSystemScript.LoadProgress().bestScore > pointsOfGame)
        {
            pointsToSave = SaveSystemScript.LoadProgress().bestScore;
        }
        SaveSystemScript.SaveProgress();
    }
}
