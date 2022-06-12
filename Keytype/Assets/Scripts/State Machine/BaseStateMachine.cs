using UnityEngine;

public abstract class BaseStateMachine : MonoBehaviour
{
    [SerializeField] private State initialState;
    public State currentState;

    protected void Awake()
    {
        currentState = initialState;
    }

    protected void Update()
    {
        currentState.Execute(this);
    }
}