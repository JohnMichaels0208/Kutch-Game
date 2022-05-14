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

    new private void Awake()
    {
        base.Awake();
    }

    new private void Update()
    {
        base.Update();
    }
}
