using UnityEngine;

public class HoverChild : MonoBehaviour, IHoverable
{
    private IHoverable parentManager;

void Awake()
{
    // Search starting from the PARENT transform to avoid finding itself
    if (transform.parent != null)
    {
        parentManager = transform.parent.GetComponentInParent<IHoverable>();
    }

    if (parentManager == null)
    {
        Debug.LogWarning($"HoverChild on {gameObject.name} found no IHoverable in parents!");
    }
}

    public void OnIsHovering(bool isHovering)
    {
        // Pass the message up to the parent
        parentManager?.OnIsHovering(isHovering);
    }
}