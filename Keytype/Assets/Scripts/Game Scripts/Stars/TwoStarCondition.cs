public class TwoStarCondition : StarBaseCondition
{
    public override int starsWhenTrue
    {
        get { return 2; }
        protected set { }
    }
    public override bool CheckCondition(object dataParam)
    {
        base.CheckCondition(dataParam);
        if (gameManager.pointsOfGame >= gameManager.pointsForOneStar * 1.5f)
        {
            return true;
        }
        return false;
    }
}
