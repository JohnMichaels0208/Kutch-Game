public abstract class StarBaseCondition : ConditionBase
{
    public abstract int starsWhenTrue { get; protected set; }
    protected GameManagerScript gameManager;
    public override bool CheckCondition(object dataParam)
    {
        gameManager = (GameManagerScript)dataParam;
        return false;
    }
}
