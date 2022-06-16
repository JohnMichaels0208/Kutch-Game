using UnityEngine;

public abstract class BaseStateMachine : MonoBehaviour
{
    [SerializeField] private State initialState;
    public State currentState { get; private set; }

    protected void Awake()
    {
        SwitchState(initialState);
    }

    protected void Update()
    {
        currentState.Execute(this);
    }

    public void SwitchState(State nextState)
    {
        currentState = nextState;
        for (int i = 0; i < currentState.actions.Length; i++)
        {
            currentState.actions[i].OnEnterState(this);
        }
        for (int i = 0; i < currentState.transitions.Length; i++)
        {
            for (int j = 0; j < currentState.transitions[i].conditions.Length; j++)
            {
                currentState.transitions[i].conditions[j].OnEnterState(this);
            }
        }
    }
}