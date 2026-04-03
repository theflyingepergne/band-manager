using UnityEngine;
using DG.Tweening;

public class SetlistPreparationManager : MonoBehaviour, IHoverable
{
    [Header("Positions")]
    [SerializeField] private Transform startAnchor;
    [SerializeField] private Transform hoverAnchor;

    [Header("Animation")]
    [SerializeField] private float tweenSpeed = 0.5f; // Bumped up slightly for better feel with Back easing
    [SerializeField] private Ease easeType = Ease.OutBack; // OutBack is usually better for "revealing"

    private bool isTweening = false;
    private bool currentHoverState = false;

    public void Start()
    {
        transform.position = startAnchor.position;
        transform.rotation = startAnchor.rotation;
    }

    public void OnIsHovering(bool isHovering)
    {
        Debug.Log("Hovering on setlist");
        // 1. If we are already moving, don't interrupt!
        if (isTweening) return;

        // 2. If the state hasn't actually changed, do nothing
        if (isHovering == currentHoverState) return;

        currentHoverState = isHovering;
        ExecuteTween(isHovering);
    }



    private void ExecuteTween(bool isHovering)
    {
        // Use the position and rotation of the anchor objects instead of hardcoded numbers
        Vector3 destPos = isHovering ? hoverAnchor.position : startAnchor.position;
        Vector3 destRot = isHovering ? hoverAnchor.eulerAngles : startAnchor.eulerAngles;

        isTweening = true;
        Sequence s = DOTween.Sequence();

        s.Join(transform.DOMove(destPos, tweenSpeed).SetEase(easeType));
        s.Join(transform.DORotate(destRot, tweenSpeed).SetEase(easeType));

        s.OnComplete(() => isTweening = false);
    }
}