public abstract class StarBaseCondition : ConditionBase
{
    public abstract float pointsCoefficientForStar { get; protected set; }
    public abstract int starsWhenTrue { get; protected set; }
    protected GameManagerScript gameManager;

    public override bool CheckCondition(object dataParam)
    {
        gameManager = (GameManagerScript)dataParam;
        if (gameManager.pointsOfGame >= gameManager.pointsForOneStar * pointsCoefficientForStar)
        {
            return true;
        }
        return false;
    }
}
