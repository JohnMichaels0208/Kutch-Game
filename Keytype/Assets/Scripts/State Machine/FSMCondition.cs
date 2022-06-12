using UnityEngine;
       
public abstract class FSMCondition : ConditionBase
{
    protected const string menuLocation = "FSM/Condition/";

    protected BaseStateMachine stateMachine;

    public override void FirstCheckConditionCall(object data)
    {
        stateMachine = (BaseStateMachine)data;
    }
}