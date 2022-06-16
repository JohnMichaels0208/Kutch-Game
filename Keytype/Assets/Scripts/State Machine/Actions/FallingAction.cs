using UnityEngine;
using TMPro;
[CreateAssetMenu(menuName = location + "FallingAction", fileName = "FallingAction", order = 1)]
public class FallingAction : Action
{
    private FallingElementStateMachine castedStateMachine;

    public override void Execute(BaseStateMachine stateMachine)
    {
        castedStateMachine = stateMachine as FallingElementStateMachine;
        castedStateMachine.transform.Translate(Vector3.down * Time.deltaTime * castedStateMachine.fallingSpeed);
        GameManagerScript.instance.keyCodesOnScreeen[castedStateMachine.associatedKeyCode] = true;
    }

    public override void OnEnterState(BaseStateMachine stateMachine)
    {
        GameManagerScript.instance.speedChangedEvent += OnSpeedChanged;
    }

    private void OnSpeedChanged(object sender, System.EventArgs eventArgs)
    {
         castedStateMachine.fallingSpeed = GameManagerScript.instance.currentFallspeed;
    }
}