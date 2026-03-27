using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    // The static reference that other scripts will call
    public static T Instance { get; private set; }

    protected virtual void Awake()
    {
        // Simply set the instance to this object when the scene starts
        Instance = this as T;
    }
}