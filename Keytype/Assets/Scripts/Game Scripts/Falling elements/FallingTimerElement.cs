using UnityEngine;

public class FallingTimerElement : FallingElementBase
{
    protected override Sound correctKeySound { get { return GameManagerScript.instance.normalLetterCorrectKeySound; } }
    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
    }

    protected override void CorrectKey()
    {
        GameManagerScript.instance.currentFallspeed = GameManagerScript.instance.timerFallSpeed;
        GameManagerScript.instance.OnTimer();
        base.CorrectKey();
    }

    protected override void Collide()
    {
        base.Collide();
        GameManagerScript.instance.currentFallspeed = GameManagerScript.instance.normalFallspeed;
    }

    protected override void TouchedByBombBlast()
    {
        base.TouchedByBombBlast();
        GameManagerScript.instance.currentFallspeed = GameManagerScript.instance.normalFallspeed;
    }
}
