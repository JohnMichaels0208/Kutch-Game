using UnityEngine;

[CreateAssetMenu(menuName = "FSM/Transition", fileName = "Transition", order = 1)]
public class Transition : ScriptableObject
{
    public State nextState;

    public ConditionBase[] conditions;

    public bool transitionOnComplete;
    public void Execute(BaseStateMachine stateMachine)
    {
        if (transitionOnComplete)
        {
            for (int i = 0; i < stateMachine.currentState.actions.Length; i++)
            {
                if (!stateMachine.currentState.actions[i].isCompleted)
                {
                    return;
                }
            }
        }
        else if (!transitionOnComplete && conditions.Length != 0)
        {
            for (int i = 0; i < conditions.Length; i++)
            {
                if (!conditions[i].CheckCondition(stateMachine))
                {
                    return;
                }
            }
        }
        stateMachine.currentState = nextState;
    }
}