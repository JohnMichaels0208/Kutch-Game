using UnityEngine;
[CreateAssetMenu(fileName = "State", menuName = "FSM/State", order = 1)]
public class State : ScriptableObject
{
    public Transition[] transitions;
    public Action[] actions;

    public virtual void Execute(BaseStateMachine stateMachine)
    {
        for (int i = 0; i < actions.Length; i++)
        {
            actions[i].FirstExecuteCallCheck(stateMachine);
        }
        for (int i = 0; i < transitions.Length; i++)
        {
            transitions[i].Execute(stateMachine);
        }
    }
}
