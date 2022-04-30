public class OneStarCondition : StarBaseCondition
{
    public override int starsWhenTrue 
    {
        get { return 1; }
        protected set { }
    }
    public override bool CheckCondition(object dataParam)
    {
        base.CheckCondition(dataParam);
        if (gameManager.pointsForOneStar <= gameManager.pointsOfGame)
        {
            return true;
        }
        return false;
    }
}
