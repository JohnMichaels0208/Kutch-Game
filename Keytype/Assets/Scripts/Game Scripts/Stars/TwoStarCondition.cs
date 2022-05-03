public class TwoStarCondition : StarBaseCondition
{
    public override float pointsCoefficientForStar { get; protected set; } = 1.5f;
    public override int starsWhenTrue
    {
        get { return 2; }
        protected set { }
    }
}
