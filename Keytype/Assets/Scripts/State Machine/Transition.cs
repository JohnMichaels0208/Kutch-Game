using UnityEngine;

[CreateAssetMenu(menuName = "FSM/Transition", fileName = "Transition", order = 1)]
public class Transition : ScriptableObject
{
    public State nextState;

    public FSMCondition[] conditions;

    public bool transitionOnComplete;

    public void Execute(BaseStateMachine stateMachine)
    {
        //Checking if state actions aren't complete
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
        //Checking if conditions are false
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
        FallingElementStateMachine fsm = (FallingElementStateMachine)stateMachine;

        Debug.Log("transition switching to" + nextState + " associated key code: " + fsm.associatedKeyCode);
        stateMachine.SwitchState(nextState);
    }
}