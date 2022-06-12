using UnityEngine;
using TMPro;
[CreateAssetMenu(menuName = location + "FallingAction", fileName = "FallingAction", order = 1)]
public class FallingAction : Action
{
    private FallingElementStateMachine castedStateMachine { get { return stateMachine as FallingElementStateMachine; } }

    public override void Execute(BaseStateMachine stateMachine)
    {
        Debug.Log("Falling action Execute");
        castedStateMachine.transform.Translate(Vector3.down * Time.deltaTime * castedStateMachine.fallingSpeed);
        GameManagerScript.instance.keyCodesOnScreeen[castedStateMachine.associatedKeyCode] = true;
        castedStateMachine.keyCodeTextGO.GetComponent<TextMeshPro>().text = castedStateMachine.fallingSpeed.ToString();
    }

    public override void FirstExecuteCall(BaseStateMachine stateMachine)
    {
        Debug.Log("falling action first execute call");
        GameManagerScript.instance.speedChangedEvent += OnSpeedChanged;
    }

    private void OnSpeedChanged(object sender, System.EventArgs eventArgs)
    {
         castedStateMachine.fallingSpeed = GameManagerScript.instance.currentFallspeed;
    }
}