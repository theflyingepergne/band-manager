using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T Instance { get; private set; }

    [Header("From Base Class")]
    [Tooltip("If true, this object will persist between scenes")]
    [SerializeField] private bool persistAcrossScenes = false;

    protected virtual void Awake()
    {
        if (Instance == null)
        {
            Instance = this as T;

            if (persistAcrossScenes)
            {
                transform.SetParent(null);
                DontDestroyOnLoad(gameObject);
            }
        }
        else
        {
            // If it's a persistent singleton, we must destroy duplicates
            if (persistAcrossScenes)
            {
                Destroy(gameObject);
            }
            else
            {
                // If it's NOT persistent, we still set Instance to the newest one
                Instance = this as T;
            }
        }
    }
}