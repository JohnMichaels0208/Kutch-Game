using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FallingElementScript : MonoBehaviour
{
    private GameObject                              explodeFX;
    private GameObject                              collisionFX;
    [HideInInspector] public GameManagerScript      gameManagerScript;
    private TextMeshPro                             textMeshProUGUI;
    [SerializeField] private GameObject             textGameObject;

    private float                                   fallingSpeed = 2f;
    private GameCharacter                           associatedGameCharacter;

    private List<GameCharacter>                     gameCharacterList;

    //explode event declaration
    public delegate void FallingElementExplodeEventHandler(object sender, System.EventArgs e);
    public event FallingElementExplodeEventHandler FallingElementExplodeEvent;

    void Start()
    {
        explodeFX = transform.GetChild(0).gameObject;
        collisionFX = transform.GetChild(1).gameObject;
        gameManagerScript.pauseGameEvent += OnDisableFallingElements;
        gameCharacterList = gameManagerScript.allGameCharacters;
        textMeshProUGUI = textGameObject.transform.GetComponent<TextMeshPro>();
        associatedGameCharacter = gameCharacterList[Random.Range(0, gameCharacterList.Count)];
        textMeshProUGUI.text = associatedGameCharacter.label.ToString(); 
    }

    void Update()
    {
        if (associatedGameCharacter.keyCode == gameManagerScript.currentKeycodeDetected)
        {
            OnFallingElementExplode();
            Die(explodeFX);
        }

        transform.Translate(Vector3.down * Time.deltaTime * fallingSpeed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Death Zone")
        {
            gameManagerScript.RemoveHealth(1);
            Die(collisionFX);
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
    private void Die(GameObject FX)
    {
        FX.SetActive(true);
        FX.transform.parent = null;
        Destroy(gameObject);
    }

    private void TogglePauseFallingElements()
    {
        if (this != null)
        {
            enabled = !enabled;
        }
    }

    protected virtual void OnFallingElementExplode()
    {
        if (FallingElementExplodeEvent != null)
        {
            FallingElementExplodeEvent.Invoke(this, System.EventArgs.Empty);
        }
    }
}
