using UnityEngine;

public abstract class CustomGameEventData : ScriptableObject
{
    // Override in children
    public abstract void Execute();    
}
