using UnityEngine;
using TMPro;

public class FallingElementStateMachine : BaseStateMachine
{
    [Header("References")]
    public GameObject keyCodeTextGO;

    public AudioSource audioSourceComponent;
    public Animator animatorComponent;
    public TextMeshPro keyCodeTMPComponent;

    public float fallingSpeed;
    public KeyCode associatedKeyCode;

    public Sound correctKeySound;

    public delegate void OnMyTriggerEnterHandler(object sender, System.EventArgs eventArgs, Collider2D collision);
    public event OnMyTriggerEnterHandler onMyTriggerEnterEvent;

    public const string animatorExplodedParamName = "Exploded";
    public const string animatorCollidedParamName = "Collided";

    new private void Awake()
    {
        base.Awake();
    }

    new private void Update()
    {
        base.Update();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        InvokeMyTriggerEnter(collision);
    }

    protected virtual void InvokeMyTriggerEnter(Collider2D collision)
    {
        onMyTriggerEnterEvent?.Invoke(this, System.EventArgs.Empty, collision);
    }
}
