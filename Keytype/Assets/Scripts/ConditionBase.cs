using UnityEngine;
public abstract class ConditionBase : ScriptableObject
{
    public abstract bool CheckCondition(object data);
}
