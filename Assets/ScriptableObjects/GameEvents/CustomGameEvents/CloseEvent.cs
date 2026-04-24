using UnityEngine;

[CreateAssetMenu(
    fileName = "CloseEvent",
    menuName = "Game Events/Custom Game Events/CloseEvent"
)]

public class CloseEvent : CustomGameEventData
{
    public override void Execute()
    {
        GameEventManager.Instance.CloseEventWindow();
    }
}