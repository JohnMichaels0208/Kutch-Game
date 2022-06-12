using UnityEngine;
[CreateAssetMenu(menuName = location + "CorrectKeyAction", fileName = "CorrectKeyAction")]
public class CorrectKeyAction : Action
{
    public override void FirstExecuteCall(BaseStateMachine stateMachine)
    {
        FallingElementStateMachine castedStateMachine = (FallingElementStateMachine)stateMachine;
        SoundManagerScript.PlaySound(castedStateMachine.audioSourceComponent, castedStateMachine.correctKeySound);
        GameManagerScript.instance.keyCodesOnScreeen[castedStateMachine.associatedKeyCode] = false;
        GameManagerScript.instance.AddPoints();
        if (castedStateMachine.animatorComponent != null)
        {
            castedStateMachine. animatorComponent.SetBool(FallingElementStateMachine.animatorExplodedParamName, true);
        }
    }
    public override void Execute(BaseStateMachine stateMachine)
    {
    }
}
