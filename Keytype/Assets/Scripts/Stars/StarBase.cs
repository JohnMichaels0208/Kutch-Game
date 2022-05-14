using UnityEngine;

public abstract class StarBase : MonoBehaviour
{
    public abstract StarBase condition { get; [SerializeField] protected set; }

    public void CheckRequirements()
    {

    }
}
