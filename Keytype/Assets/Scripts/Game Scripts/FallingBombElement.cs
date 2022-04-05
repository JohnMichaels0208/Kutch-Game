using UnityEngine;
public class FallingBombElement : FallingElementBase
{
    [SerializeField] private GameObject particleGO;
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
    public override void OnFXAnimationEnd()
    {
        base.OnFXAnimationEnd();
    }
    protected override void Collide()
    {
        SoundManagerScript.PlaySound(base.audioSourceComponent, GameManagerScript.instance.collideSound);
        base.Collide();
    }
    protected override void Blast()
    {
        CorrectKey();
    }
    protected override void CorrectKey()
    {
        SoundManagerScript.PlaySound(base.audioSourceComponent, GameManagerScript.instance.bombLetterCorrectKeySound);
        if (particleGO != null)
        {
            particleGO.GetComponent<ParticleSystem>().Play();
        }
        base.CorrectKey();

    }
}
