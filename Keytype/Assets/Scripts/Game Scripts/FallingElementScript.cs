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
        SoundManagerScript.PlaySound(base.audioSourceComponent, GameManagerScript.instance.collideSound);
        base.Collide();
    }
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
    }

    protected override void CorrectKey()
    {
        SoundManagerScript.PlaySound(base.audioSourceComponent, GameManagerScript.instance.normalLetterCorrectKeySound);
        base.CorrectKey();
    }

    protected override void Blast()
    {
        base.CorrectKey();
    }

    public override void OnFXAnimationEnd()
    {
        base.OnFXAnimationEnd();
    }

    public void FallingElementCorrectKey()
    {
        Debug.Log("falling elemetn correct key");
    }
}