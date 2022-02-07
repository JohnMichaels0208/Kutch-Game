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
    public GameCharacter                           associatedGameCharacter;

    public delegate void FallingElementCollideEventHandler(object sender, System.EventArgs eventArgs);
    public event FallingElementCollideEventHandler fallingElementCollideEvent;

    void Start()
    {
        explodeFX = transform.GetChild(0).gameObject;
        collisionFX = transform.GetChild(1).gameObject;
        gameManagerScript.pauseGameEvent += OnDisableFallingElements;
        textMeshProUGUI = textGameObject.transform.GetComponent<TextMeshPro>();
        textMeshProUGUI.text = associatedGameCharacter.label.ToString();
    }

    void Update()
    {
        transform.Translate(Vector3.down * Time.deltaTime * fallingSpeed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Death Zone")
        {
            Collide();
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

    public void Explode()
    {
        explodeFX.SetActive(true);
        explodeFX.transform.parent = null;
        Destroy(gameObject);
    }

    private void Collide()
    {
        OnFallingElementCollide();
        collisionFX.SetActive(true);
        collisionFX.transform.parent = null;
        Destroy(gameObject);
    }

    private void TogglePauseFallingElements()
    {
        if (this != null)
        {
            enabled = !enabled;
        }
    }

    protected virtual void OnFallingElementCollide()
    {
        if (fallingElementCollideEvent != null)
        {
            fallingElementCollideEvent.Invoke(this, System.EventArgs.Empty);
        }
    }
}
