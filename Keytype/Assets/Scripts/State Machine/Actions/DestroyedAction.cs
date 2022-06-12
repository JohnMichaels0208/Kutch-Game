using UnityEngine;

[CreateAssetMenu(menuName = location + "DestroyedAction", fileName = "DestroyedAction")]
public class DestroyedAction : Action
{
    public override void FirstExecuteCall(BaseStateMachine stateMachine)
    {
        FallingElementStateMachine castedStateMachine = stateMachine as FallingElementStateMachine;
        SoundManagerScript.PlaySound(castedStateMachine.audioSourceComponent, GameManagerScript.instance.collideSound);
        GameManagerScript.instance.keyCodesOnScreeen[castedStateMachine.associatedKeyCode] = false;
        if (castedStateMachine.animatorComponent != null)
        {
            castedStateMachine.animatorComponent.SetBool(FallingElementStateMachine.animatorCollidedParamName, true);
        }
        GameManagerScript.instance.RemoveHealth(GameManagerScript.instance.collidedMistakeText);
    }

    public override void Execute(BaseStateMachine stateMachine)
    {
    }
}
