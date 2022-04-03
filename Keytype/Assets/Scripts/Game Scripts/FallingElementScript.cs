using UnityEngine;
public class FallingElementScript : FallingElementBase
{
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

    protected override void Collide()
    {
        base.Collide();
        SoundManagerScript.PlaySound(GameManagerScript.instance.audioSource, GameManagerScript.instance.collideSound);
    }
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
    }

    protected override void CorrectKey()
    {
        base.CorrectKey();
        SoundManagerScript.PlaySound(GameManagerScript.instance.audioSource, GameManagerScript.instance.normalLetterCorrectKeySound);
    }

    protected override void Blast()
    {
        base.Blast();
    }

    public override void OnFXAnimationEnd()
    {
        base.OnFXAnimationEnd();
    }
}