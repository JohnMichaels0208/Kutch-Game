using UnityEngine;

public abstract class Action : ScriptableObject
{
    public bool isCompleted { get; set; } = false;
    public abstract void Execute(BaseStateMachine stateMachine);
}
