using UnityEngine;
[CreateAssetMenu(menuName = "FSM/Action/FallingAction", fileName = "FallingAction", order = 1)]
public class FallingAction : Action
{
    public override void Execute(BaseStateMachine stateMachine)
    {
        FallingElementStateMachine castedStateMachine = (FallingElementStateMachine)stateMachine;
    }

    private void OnKeyDown(object sender, System.EventArgs eventArgs)
    {

    }
}
