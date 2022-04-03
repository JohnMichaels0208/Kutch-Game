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
        base.Collide();
        SoundManagerScript.PlaySound(GameManagerScript.instance.audioSource, GameManagerScript.instance.collideSound);
    }
    protected override void Blast()
    {
        CorrectKey();
        base.Blast();
    }
    protected override void CorrectKey()
    {
        base.CorrectKey();
        SoundManagerScript.PlaySound(GameManagerScript.instance.audioSource, GameManagerScript.instance.bombLetterCorrectKeySound);
        if (particleGO != null)
        {
            particleGO.GetComponent<ParticleSystem>().Play();
        }
    }
}
