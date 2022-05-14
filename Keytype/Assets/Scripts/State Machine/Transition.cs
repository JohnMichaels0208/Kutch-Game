using UnityEngine;

[CreateAssetMenu(menuName = "FSM/Transition", fileName = "Transition", order = 1)]
public class Transition : ScriptableObject
{
    public State nextState;

    public ConditionBase[] conditions;

    public bool transitionOnComplete;
    public void Execute(BaseStateMachine stateMachine)
    {
        if (conditions.Length != 0)
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
            if (!transitionOnComplete)
            {
                for (int i = 0; i < conditions.Length; i++)
                {
                    if (!conditions[i].CheckCondition(stateMachine))
                    {
                        return;
                    }
                }
            }
        }
        else if (conditions.Length == 0 && transitionOnComplete)
        {
            for (int i = 0; i < stateMachine.currentState.actions.Length; i++)
            {
                if (!stateMachine.currentState.actions[i].isCompleted)
                {
                    return;
                }
            }
        }
        stateMachine.currentState = nextState;
    }
}
