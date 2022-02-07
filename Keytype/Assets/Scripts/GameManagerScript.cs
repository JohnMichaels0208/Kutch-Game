using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    [SerializeField] private AudioClip explodeAudioClip;
    [SerializeField] private AudioClip collideAudioClip;
    [SerializeField] private AudioClip gameOverAudioClip;

    //Reference to parent of ui hearts
    [SerializeField] private Transform heartsUIParent;

    [SerializeField] private GameObject heartUI;

    [SerializeField] private GameObject pauseUI;

    [SerializeField] private GameObject gameOverUI;

    private AudioSource audioSource;
    
    // Reference to the object to spawn
    [SerializeField] private Transform fallingElement;

    //Spawn Position Varibles
    private const float spawnYPosition = 6;
    private const float minSpawnXPosition = 7.8f;
    private const float maxSpawnXPosition = -7.8f;

    //Countdown Variables
    private float countdownAmt = 0.5f;
    private float countdownElapsed;
    private int spawnAmtPerCountdown = 1;
    private bool countdownComplete;

    //All game characters
    private readonly GameCharacter gameCharacterA = new GameCharacter('A', KeyCode.A);
    private readonly GameCharacter gameCharacterB = new GameCharacter('B', KeyCode.B);
    private readonly GameCharacter gameCharacterC = new GameCharacter('C', KeyCode.C);
    private readonly GameCharacter gameCharacterD = new GameCharacter('D', KeyCode.D);
    private readonly GameCharacter gameCharacterE = new GameCharacter('E', KeyCode.E);
    private readonly GameCharacter gameCharacterF = new GameCharacter('F', KeyCode.F);
    private readonly GameCharacter gameCharacterG = new GameCharacter('G', KeyCode.G);
    private readonly GameCharacter gameCharacterH = new GameCharacter('H', KeyCode.H);
    private readonly GameCharacter gameCharacterI = new GameCharacter('I', KeyCode.I);
    private readonly GameCharacter gameCharacterJ = new GameCharacter('J', KeyCode.J);
    private readonly GameCharacter gameCharacterK = new GameCharacter('K', KeyCode.K);
    private readonly GameCharacter gameCharacterL = new GameCharacter('L', KeyCode.L);
    private readonly GameCharacter gameCharacterM = new GameCharacter('M', KeyCode.M);
    private readonly GameCharacter gameCharacterN = new GameCharacter('N', KeyCode.N);
    private readonly GameCharacter gameCharacterO = new GameCharacter('O', KeyCode.O);
    private readonly GameCharacter gameCharacterP = new GameCharacter('P', KeyCode.P);
    private readonly GameCharacter gameCharacterQ = new GameCharacter('Q', KeyCode.Q);
    private readonly GameCharacter gameCharacterR = new GameCharacter('R', KeyCode.R);
    private readonly GameCharacter gameCharacterS = new GameCharacter('S', KeyCode.S);
    private readonly GameCharacter gameCharacterT = new GameCharacter('T', KeyCode.T);
    private readonly GameCharacter gameCharacterU = new GameCharacter('U', KeyCode.U);
    private readonly GameCharacter gameCharacterV = new GameCharacter('V', KeyCode.V);
    private readonly GameCharacter gameCharacterW = new GameCharacter('W', KeyCode.W);
    private readonly GameCharacter gameCharacterX = new GameCharacter('X', KeyCode.X);
    private readonly GameCharacter gameCharacterY = new GameCharacter('Y', KeyCode.Y);
    private readonly GameCharacter gameCharacterZ = new GameCharacter('Z', KeyCode.Z);

    //List containing all game characters
    public List<GameCharacter> allGameCharacters;

    [HideInInspector] public KeyCode currentKeycodeDetected;

    [HideInInspector] public int lives = 1;

    private bool gameOver;
    private bool gamePaused = false;

    public delegate void PauseGameEventHandler(object sender, System.EventArgs eventArgs);
    public event PauseGameEventHandler pauseGameEvent;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        countdownElapsed = countdownAmt;
        allGameCharacters = new List<GameCharacter>()
        {
            gameCharacterA,
            gameCharacterB,
            gameCharacterC,
            gameCharacterD,
            gameCharacterE,
            gameCharacterF,
            gameCharacterG,
            gameCharacterH,
            gameCharacterI,
            gameCharacterJ,
            gameCharacterK,
            gameCharacterL,
            gameCharacterM,
            gameCharacterN,
            gameCharacterO,
            gameCharacterP,
            gameCharacterQ,
            gameCharacterR,
            gameCharacterS,
            gameCharacterT,
            gameCharacterU,
            gameCharacterV,
            gameCharacterW,
            gameCharacterX,
            gameCharacterY,
            gameCharacterZ,
        };
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
        }

    }

    private void SpawnLetter()
    {
        for (int i = 0; i < spawnAmtPerCountdown; i++)
        {
            FallingElementScript instantiatedOBjectFallingElementScript = GameObject.Instantiate(fallingElement, new Vector3(Random.Range(minSpawnXPosition, maxSpawnXPosition), spawnYPosition, 0), Quaternion.identity).GetComponent<FallingElementScript>();
            instantiatedOBjectFallingElementScript.gameManagerScript = this;
            instantiatedOBjectFallingElementScript.FallingElementExplodeEvent += OnFallingElementExplode;
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

    public void RemoveHealth(int numberOfHealthsToRemove)
    {
        audioSource.clip = collideAudioClip;
        audioSource.Play();
        lives -= numberOfHealthsToRemove;
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
        audioSource.clip = gameOverAudioClip;
        audioSource.Play();
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
    
    private void OnFallingElementExplode(object sender, System.EventArgs eventArgs)
    {
        audioSource.clip = explodeAudioClip;
        audioSource.Play();
    }
}
