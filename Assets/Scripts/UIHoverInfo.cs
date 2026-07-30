using System;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class UIHoverInfo : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    // Unified events for any UI hover trigger
    public static event Action<string> OnHoverStarted;
    public static event Action OnHoverEnded;

    protected void TriggerHover(string identifier)
    {
        OnHoverStarted?.Invoke(identifier);
    }

    protected void ClearHover()
    {
        OnHoverEnded?.Invoke();
    }

    public virtual void OnPointerEnter(PointerEventData eventData) { }
    public virtual void OnPointerExit(PointerEventData eventData) => ClearHover();
}