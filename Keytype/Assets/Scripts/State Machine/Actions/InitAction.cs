using UnityEngine;
using TMPro;
[CreateAssetMenu(fileName = "InitAction", menuName = location + "InitAction")]
public class InitAction : Action
{
    public override void Execute(BaseStateMachine stateMachine)
    {
    }

    public override void FirstExecuteCall(BaseStateMachine stateMachine)
    {
        FallingElementStateMachine castedStateMachine = stateMachine as FallingElementStateMachine;
        castedStateMachine.audioSourceComponent = castedStateMachine.GetComponent<AudioSource>();
        castedStateMachine.animatorComponent = castedStateMachine.GetComponent<Animator>();
        castedStateMachine.keyCodeTMPComponent = castedStateMachine.keyCodeTextGO.GetComponent<TextMeshPro>();

        castedStateMachine.associatedKeyCode = GameManagerScript.instance.keyCodes[Random.Range(0, GameManagerScript.instance.keyCodes.Length)];
        castedStateMachine.fallingSpeed = Random.Range(GameManagerScript.instance.currentFallspeed - GameManagerScript.instance.fallSpeedDifference / 2, GameManagerScript.instance.currentFallspeed + GameManagerScript.instance.fallSpeedDifference / 2);

        castedStateMachine.keyCodeTMPComponent.text = castedStateMachine.associatedKeyCode.ToString();
        isCompleted = true;
    }
}