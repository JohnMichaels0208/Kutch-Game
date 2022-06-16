using UnityEngine;
public abstract class FSMCondition : ScriptableObject
{
    protected const string menuLocation = "FSM/Condition/";

    public virtual void OnEnterState(BaseStateMachine stateMachine)
    {
    }

    public abstract bool CheckCondition(BaseStateMachine stateMachine);
}