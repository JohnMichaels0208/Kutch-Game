using UnityEngine;
public abstract class ConditionBase : ScriptableObject
{
    private bool isFirstCall = true;

    public abstract bool CheckCondition(object data);

    public void EnableFirstCall(object data)
    {
        if (isFirstCall)
        {
            FirstCheckConditionCall(data);
        }
        isFirstCall = false;
    }

    public virtual void FirstCheckConditionCall(object data) { }
}