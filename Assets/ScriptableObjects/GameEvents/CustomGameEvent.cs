using UnityEngine;

public abstract class CustomGameEvent : ScriptableObject
{
    // Override in children
    public abstract void Execute();    
}
