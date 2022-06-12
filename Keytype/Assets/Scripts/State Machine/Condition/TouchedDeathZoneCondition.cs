using UnityEngine;
[CreateAssetMenu(menuName = menuLocation + "TouchedDeathZoneCondition", fileName = "TouchedByDeathZoneCondition", order = 1)]
public class TouchedDeathZoneCondition : FSMCondition
{
    private bool touchedDeathZone = false;
    public override void FirstCheckConditionCall(object data)
    {
        EnableFirstCall(data);
        base.FirstCheckConditionCall(data);
        FallingElementStateMachine castedStateMachine = stateMachine as FallingElementStateMachine;
        castedStateMachine.onMyTriggerEnterEvent += OnMyTriggerEnter;
    }

    public override bool CheckCondition(object data)
    {
        return touchedDeathZone;
    }

    private void OnMyTriggerEnter(object sender, System.EventArgs eventArgs, Collider2D collider)
    {
        if (collider.tag == "Death Zone")
        {
            touchedDeathZone = true;
        }
    }
}