using UnityEngine;

[System.Serializable]
public class CloseEvent : CustomGameEvent
{
    public override void Execute()
    {
        GameEventManager.Instance.CloseEventWindow();
    }
}