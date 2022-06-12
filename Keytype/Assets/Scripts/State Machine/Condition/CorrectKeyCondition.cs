using UnityEngine;
[CreateAssetMenu(menuName = menuLocation + "CorrectKeyCondition", fileName = "CorrectKeyCondition", order = 1)]
public class CorrectKeyCondition : FSMCondition
{
    private bool isCorrectKey = false;

    public override bool CheckCondition(object data)
    {
        EnableFirstCall(data);

        return isCorrectKey;
    }

    public override void FirstCheckConditionCall(object data)
    {
        base.FirstCheckConditionCall(data);
        GameManagerScript.instance.keyDownEvent += OnKeyDown;
    }

    private void OnKeyDown(object sender, System.EventArgs eventArgs, KeyCode keyCode)
    {
        FallingElementStateMachine castedStateMachine = (FallingElementStateMachine)stateMachine;
        if (keyCode == castedStateMachine.associatedKeyCode) isCorrectKey = true;
    }
}