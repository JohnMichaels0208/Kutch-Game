using UnityEngine;
public class FallingElementScript : FallingElementBase
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

}