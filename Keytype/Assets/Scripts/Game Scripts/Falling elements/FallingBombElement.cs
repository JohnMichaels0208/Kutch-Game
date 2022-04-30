using UnityEngine;
public class FallingBombElement : FallingElementBase
{
    protected override Sound correctKeySound { get { return GameManagerScript.instance.bombLetterCorrectKeySound; } }

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
    protected override void TouchedByBombBlast()
    {
        CorrectKey();
    }
    protected override void CorrectKey()
    {
        if(particleGO!=null) particleGO.GetComponent<ParticleSystem>().Play();
        base.CorrectKey();
    }
}
