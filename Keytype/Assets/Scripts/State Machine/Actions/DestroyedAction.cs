using UnityEngine;

[CreateAssetMenu(menuName = location + "DestroyedAction", fileName = "DestroyedAction")]
public class DestroyedAction : Action
{
    public override void OnEnterState(BaseStateMachine stateMachine)
    {
        FallingElementStateMachine castedStateMachine = stateMachine as FallingElementStateMachine;
        Debug.Log("entering destroyed action, associated key code: " + castedStateMachine.associatedKeyCode);
        SoundManagerScript.PlaySound(castedStateMachine.audioSourceComponent, GameManagerScript.instance.collideSound);
        GameManagerScript.instance.keyCodesOnScreeen[castedStateMachine.associatedKeyCode] = false;
        if (castedStateMachine.animatorComponent != null)
        {
            castedStateMachine.animatorComponent.SetBool(FallingElementStateMachine.animatorCollidedParamName, true);
        }
        GameManagerScript.instance.RemoveHealth(GameManagerScript.instance.collidedMistakeText);
    }
}
