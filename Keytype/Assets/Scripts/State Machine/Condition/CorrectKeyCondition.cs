using UnityEngine;
[CreateAssetMenu(menuName = menuLocation + "CorrectKeyCondition", fileName = "CorrectKeyCondition", order = 1)]
public class CorrectKeyCondition : FSMCondition
{
    private bool isKeyDown = false;
    private KeyCode keyCodeDetected = KeyCode.None;

    private FallingElementStateMachine castedStateMachine;

    public override bool CheckCondition(BaseStateMachine stateMachine)
    {
        if (isKeyDown)
        {
            castedStateMachine = (FallingElementStateMachine)stateMachine;
            if (castedStateMachine.associatedKeyCode == keyCodeDetected)
            {
                isKeyDown = false;
                keyCodeDetected = KeyCode.None;
                return true;
            }
        }
        return false;
    }

    public override void OnEnterState(BaseStateMachine stateMachine)
    {
        castedStateMachine = (FallingElementStateMachine)stateMachine;
        GameManagerScript.instance.keyDownEvent += OnKeyDown;
    }

    private void OnKeyDown(object sender, System.EventArgs eventArgs, KeyCode keyCode)
    {
        keyCodeDetected = keyCode;
        isKeyDown = true;
    }
}