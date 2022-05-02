using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using TMPro;
using System.Linq;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour
{
    public static GameManagerScript instance;

    public Sound collideSound;
    public Sound normalLetterCorrectKeySound;
    public Sound bombLetterCorrectKeySound;

    [SerializeField] private Sound gameOverSound;
    [SerializeField] private Sound gameEndedSound;

    //Reference to parent of ui elements
    [SerializeField] private Transform heartUIParent;

    [SerializeField] private GameObject heartUI;

    [SerializeField] private GameObject pauseUI;

    [SerializeField] private GameObject gameOverUI;

    [SerializeField] private GameObject optionsUI;

    [SerializeField] private GameObject pointsUI;

    [SerializeField] private GameObject mistakeText;

    [SerializeField] private AudioMixer audioMixer;

    [SerializeField] private GameObject levelCompleteUI;

    private TextMeshProUGUI mistakeDisplayerTextComponent;
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

    public KeyCode[] keyCodes;

    //Spawn Position Varibles
    private const float spawnYPosition = 5f;
    private const float minSpawnXPosition = 7.8f;
    private const float maxSpawnXPosition = -7.8f;

    //Fall speed of the falling elements
    public float currentFallspeed;
    public float normalFallspeed = 2;
    public float timerFallSpeed = 0.2f;
    public float fallSpeedDifference = 1f;

    //Countdown Variables
    [SerializeField] private float countdownAmt = 0.5f;
    private float timerElapsed;

    private float pointsPerLetterExplode = 10;

    public Dictionary<KeyCode, bool> keyCodesOnScreeen = new Dictionary<KeyCode, bool>() { };
    private bool isKeyCodeDetectedOnScreen;

    private KeyCode currentKeyCodeDetected;

    private KeyCode[] exceptionKeyCodes = new KeyCode[] { KeyCode.Mouse0, KeyCode.Mouse1, KeyCode.Mouse2, KeyCode.Escape };


    private bool gameEnded;
    private bool gamePaused = false;

    //event when game is paused
    public delegate void PauseGameEventHandler(object sender, System.EventArgs eventArgs);
    public event PauseGameEventHandler pauseGameEvent;

    //event where a key is pressed down
    public delegate void KeyDownEventHandler(object sender, System.EventArgs eventArgs, KeyCode keyCodeDetected);
    public event KeyDownEventHandler keyDownEvent;

    public delegate void SpeedChangedEventHandler(object sender, System.EventArgs eventArgs);
    public event SpeedChangedEventHandler speedChangedEvent;

    //acceleration speed when falling element spawned
    public readonly float accelerationSpeed = 0.5f;
    [HideInInspector] public int lives = 1;

    public float pointsForOneStar;
    public int starsOfGame { get; private set; } = 0;

    private LevelData associatedLevelData;
    private int associatedLevelDataindex;

    private OneStarCondition oneStarCondition = new OneStarCondition();
    private TwoStarCondition twoStarCondition = new TwoStarCondition();
    private ThreeStarCondition threeStarCondition = new ThreeStarCondition();

    protected StarBaseCondition[] starConditions;

    //game data
    public float pointsOfGame { get; private set; } = 0;

    private void Awake()
    {
        currentFallspeed = normalFallspeed;
        starConditions = new StarBaseCondition[3] {oneStarCondition, twoStarCondition, threeStarCondition};
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
        mistakeDisplayerTextComponent = mistakeText.GetComponent<TextMeshProUGUI>();
        audioSource = GetComponent<AudioSource>();
        List<LevelData> levelDatas = SaveSystemScript.LoadLevelDataList();
        associatedLevelDataindex = SaveSystemScript.LoadLevelIndexBySceneName(SceneManager.GetActiveScene().name);
        associatedLevelData = levelDatas[associatedLevelDataindex];
    }

    private void Start()
    {
        pointsForOneStar = associatedLevelData.levelPointsForOneStar;

        for (int i = 0; i < keyCodes.Length; i++)
        {
            keyCodesOnScreeen.Add(keyCodes[i], false);
        }
        timerElapsed = countdownAmt;
        for (int i = 1; i < lives; i++)
        {
            Instantiate(heartUI, heartUIParent);
        }
        SoundManagerScript.UpdateAudioMixerGroupVolume(audioMixer, SoundManagerScript.soundEffectGroupName, SaveSystemScript.LoadOption().soundEffectsVolume);
        SoundManagerScript.UpdateAudioMixerGroupVolume(audioMixer, SoundManagerScript.gameSoundGroupName, SaveSystemScript.LoadOption().gameSoundVolume);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePauseGame();
        }
        if (!gameEnded && !gamePaused)
        {
            currentKeyCodeDetected = GetCurrentInputKeycode();
            timerElapsed -= Time.deltaTime;
            if (timerElapsed <= 0)
            {
                SpawnLetter(GetRandomFallingElement(fallingElementAndPropabilityOfGame));
                timerElapsed = countdownAmt;
            }
            if (currentKeyCodeDetected != KeyCode.None)
            {
                if (keyCodesOnScreeen.TryGetValue(currentKeyCodeDetected, out isKeyCodeDetectedOnScreen) && !isKeyCodeDetectedOnScreen || !keyCodesOnScreeen.TryGetValue(currentKeyCodeDetected, out isKeyCodeDetectedOnScreen))
                {
                    instance.RemoveHealth(instance.wrongKeyMistakeText);
                }
                else if (keyCodesOnScreeen.TryGetValue(currentKeyCodeDetected, out isKeyCodeDetectedOnScreen) && isKeyCodeDetectedOnScreen)
                {
                    OnKeyDown(currentKeyCodeDetected);
                }
            }
        }
    }

    private Transform GetRandomFallingElement(FallingElementPropability[] fallingElementPropabilitiesToSelect)
    {
        if (fallingElementAndPropabilityOfGame.Length == 0)
        {
            return null;
        }
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
        Instantiate(transformToInstantiate, new Vector3(Random.Range(minSpawnXPosition, maxSpawnXPosition), spawnYPosition, 0), Quaternion.identity, transform);
    }
    private KeyCode GetCurrentInputKeycode()
    {
        foreach (KeyCode kcode in System.Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKeyDown(kcode) && !exceptionKeyCodes.Contains(kcode)) return kcode;
        }
        return KeyCode.None;
    }
    public void RemoveHealth(string reasonText)
    {
        mistakeText.SetActive(true);
        mistakeDisplayerTextComponent.text = reasonText;
        if (lives > 0)
        {
            lives--;
            Destroy(heartUIParent.GetChild(0).gameObject);
        }
        if (lives <= 0)
        {
            UpdateStarsOfGame();
            if (starsOfGame <= 0) EndGame(gameOverSound, gameOverUI);
            else if (starsOfGame > 0) EndGame(gameEndedSound, levelCompleteUI);
            lives = 0;
        }
    }

    private void UpdateStarsOfGame()
    {
        if (starsOfGame < starConditions.Length && starConditions[starsOfGame].CheckCondition(this)) starsOfGame = starConditions[starsOfGame].starsWhenTrue;
    }

    private void EndGame(Sound soundToPlay, GameObject uiToSetActive)
    {
        SaveSystemScript.SaveLevelProgress(associatedLevelDataindex, pointsOfGame, starsOfGame);
        OnDisableFallingElements();
        SoundManagerScript.PlaySound(audioSource, soundToPlay);
        gameEnded = true;
        uiToSetActive.SetActive(true);
    }
    protected virtual void OnDisableFallingElements()
    {
        pauseGameEvent?.Invoke(this, System.EventArgs.Empty);
            
    }
    protected virtual void OnKeyDown(KeyCode keyCodeEventData)
    {
        if (keyDownEvent != null) keyDownEvent.Invoke(this, System.EventArgs.Empty, keyCodeEventData);
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
        UpdateStarsOfGame();
        pointsOfGame += pointsPerLetterExplode;
        pointsUI.GetComponent<TextMeshProUGUI>().text = "Points: " + pointsOfGame;
    }

    public virtual void InvokeSpeedChangedEvent(float targetSpeed)
    {
        speedChangedEvent?.Invoke(this, System.EventArgs.Empty);
    }
}
