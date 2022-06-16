using UnityEngine;

public abstract class Action : ScriptableObject
{
    public bool isCompleted { get; set; } = false;

    protected const string location = "FSM/Action/";

    public virtual void Execute(BaseStateMachine stateMachine)
    {
    }

    public virtual void OnEnterState(BaseStateMachine stateMachine)
    {
    }
}
