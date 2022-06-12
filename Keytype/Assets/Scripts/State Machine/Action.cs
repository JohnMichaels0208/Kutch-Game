using UnityEngine;

public abstract class Action : ScriptableObject
{
    private bool firstCall = true;

    public bool isCompleted { get; set; } = false;

    protected BaseStateMachine stateMachine;

    protected const string location = "FSM/Action/";

    public void FirstExecuteCallCheck(BaseStateMachine stateMachine)
    {
        Debug.Log(firstCall);
        if (firstCall)
        {
            Debug.Log("first call condition true on base");
            this.stateMachine = stateMachine;
            FirstExecuteCall(stateMachine);
            firstCall = false;
        }
        else if (!firstCall)
        {
            Debug.Log("first call condition false on base");
            Execute(stateMachine);
        }
    }

    public abstract void Execute(BaseStateMachine stateMachine);

    public abstract void FirstExecuteCall(BaseStateMachine stateMachine);
}
