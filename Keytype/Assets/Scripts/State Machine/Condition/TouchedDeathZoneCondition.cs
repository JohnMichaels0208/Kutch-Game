using UnityEngine;
[CreateAssetMenu(menuName = menuLocation + "TouchedDeathZoneCondition", fileName = "TouchedByDeathZoneCondition", order = 1)]
public class TouchedDeathZoneCondition : FSMCondition
{
    private bool touchedDeathZone = false;
    public override void OnEnterState(BaseStateMachine stateMachine)
    {
        touchedDeathZone = false;
        FallingElementStateMachine castedStateMachine = stateMachine as FallingElementStateMachine;
        castedStateMachine.onMyTriggerEnterEvent += OnMyTriggerEnter;
    }

    public override bool CheckCondition(BaseStateMachine stateMachine)
    {
        FallingElementStateMachine castedStateMachine = stateMachine as FallingElementStateMachine ;
        if (touchedDeathZone) Debug.Log("trigger entered with death zone. associated key code: " + castedStateMachine.associatedKeyCode);
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