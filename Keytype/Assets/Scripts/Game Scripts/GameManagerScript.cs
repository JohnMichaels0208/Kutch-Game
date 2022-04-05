using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using TMPro;
using System.Linq;

public class GameManagerScript : MonoBehaviour
{
    public static GameManagerScript instance;

    public Sound collideSound;
    public Sound normalLetterCorrectKeySound;
    public Sound bombLetterCorrectKeySound;

    [SerializeField] private Sound gameOverSound;

    //Reference to parent of ui elements
    [SerializeField] private Transform heartUIParent;

    [SerializeField] private GameObject heartUI;

    [SerializeField] private GameObject pauseUI;

    [SerializeField] private GameObject gameOverUI;

    [SerializeField] private GameObject optionsUI;

    [SerializeField] private GameObject pointsUI;

    [SerializeField] private GameObject mistakeText;
    private TextMeshProUGUI mistakeTMP;
    public readonly string wrongKeyMistakeText = "Wrong Key Pressed!";
    public readonly string collidedMistakeText = "Text Block Collided!";

    [HideInInspector] public AudioSource audioSource;

    [System.Serializable]
    public struct FallingElementPropability
    {
        public Transform fallingElement;
        public int propablility;
    }

    public FallingElementPropability[] fallingElementAndPropabilityOfGame;

    [SerializeField] private AudioMixer audioMixer;
    //Spawn Position Varibles
    private const float spawnYPosition = 5f;
    private const float minSpawnXPosition = 7.8f;
    private const float maxSpawnXPosition = -7.8f;

    //Fall speed of the falling elements
    public float averageFallspeed = 2;
    public float fallSpeedDifference = 1f;

    //Countdown Variables
    [SerializeField] private float countdownAmt = 0.5f;
    private float timerElapsed;

    private float pointsPerLetterExplode = 10;

    //List containing all game keycodes
    public KeyCode[] keyCodes;

    public Dictionary<KeyCode, bool> keyCodesOnScreeen = new Dictionary<KeyCode, bool>() { };
    private bool isKeyCodeDetectedOnScreen;

    private KeyCode currentKeyCodeDetected;

    private KeyCode[] exceptionKeyCodes = new KeyCode[] { KeyCode.Mouse0, KeyCode.Mouse1, KeyCode.Mouse2, KeyCode.Escape };

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
    public readonly float accelerationSpeed = 2f;

    //game data
    public float pointsOfGame { get; private set; } = 0;
    [HideInInspector] public float pointsToSave;

    [SerializeField] private GameObject keyCodesOnScreenDebugger;
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
        mistakeTMP = mistakeText.GetComponent<TextMeshProUGUI>();
        for (int i = 0; i < keyCodes.Length; i++)
        {
            keyCodesOnScreeen.Add(keyCodes[i], false);
        }
        audioSource = GetComponent<AudioSource>();
        timerElapsed = countdownAmt;
        for (int i = 1; i < lives; i++)
        {
            Instantiate(heartUI, heartUIParent);
        }
        SoundManagerScript.UpdateAudioMixerGroupVolume(audioMixer, SaveSystemScript.LoadOption().soundEffectsVolume);
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
                SpawnLetter(GetRandomFallingElement(fallingElementAndPropabilityOfGame));
                timerElapsed = countdownAmt;
            }
            string stringToDisplay = "";
            foreach (KeyValuePair<KeyCode, bool> kvp in keyCodesOnScreeen)
            {
                stringToDisplay += "Key: " + kvp.Key + ", Value: " + kvp.Value + "\n";
            }
            keyCodesOnScreenDebugger.GetComponent<TextMeshProUGUI>().text = stringToDisplay;
            if (currentKeyCodeDetected != KeyCode.None)
            {
                OnKeyDown(currentKeyCodeDetected);
                if (!keyCodesOnScreeen.TryGetValue(currentKeyCodeDetected, out isKeyCodeDetectedOnScreen) || isKeyCodeDetectedOnScreen == false)
                {
                    RemoveHealth(wrongKeyMistakeText);
                }
            }
        }
    }

    private Transform GetRandomFallingElement(FallingElementPropability[] fallingElementPropabilitiesToSelect)
    {
        int totalLength = 0;
        for (int i = 0; i<fallingElementAndPropabilityOfGame.Length; i++)
        {
             totalLength += fallingElementAndPropabilityOfGame[i].propablility;
        }
        
        Transform[] fallingElementsWithProbability = new Transform[totalLength];

        int index = 0;

        for (int j = 0; j < fallingElementPropabilitiesToSelect.Length; j++)
        {
            for (int i = 0; i < fallingElementPropabilitiesToSelect[j].propablility; i++)
            {
                fallingElementsWithProbability[index] = fallingElementPropabilitiesToSelect[j].fallingElement;
                index++;
            }
        }
        int randomIndex = Random.Range(0, fallingElementsWithProbability.Length);
        return fallingElementsWithProbability[randomIndex];
    }

    private void SpawnLetter(Transform transformToInstantiate)
    {
        Instantiate(transformToInstantiate, new Vector3(Random.Range(minSpawnXPosition, maxSpawnXPosition), spawnYPosition, 0), Quaternion.identity);
    }
    private KeyCode GetCurrentInputKeycode()
    {
        foreach (KeyCode kcode in System.Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKeyDown(kcode) && !exceptionKeyCodes.Contains(kcode))
            {
                return kcode;
            }
        }
        return KeyCode.None;
    }
    public void RemoveHealth(string reasonText)
    {
        mistakeText.SetActive(true);
        mistakeTMP.text = reasonText;
        lives --;
        if (lives <= 0)
        {
            EndGame();
            lives = 0;
        }
        if (heartUIParent.childCount > 0)
        {
            Destroy(heartUIParent.GetChild(0).gameObject);
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
