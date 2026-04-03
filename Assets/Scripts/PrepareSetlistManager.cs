using UnityEngine;
using DG.Tweening;

public class SetlistPreparationManager : MonoBehaviour, IHoverable
{
    [Header("Positioning")]
    [SerializeField] private Vector3 startPosition = new Vector3(10.02f, -1.14f, 0f);
    [SerializeField] private Vector3 startRotation = new Vector3(0f, 0f, 5.41f);
    [SerializeField] private Vector3 hoverPosition = new Vector3(4.66f, -1.57f, 0f);
    [SerializeField] private Vector3 hoverRotation = new Vector3(0f, 0f, 12.295f);
    
    [Header("Animation")]
    [SerializeField] private float tweenSpeed = 0.5f; // Bumped up slightly for better feel with Back easing
    [SerializeField] private Ease easeType = Ease.OutBack; // OutBack is usually better for "revealing"

    private bool isTweening = false;
    private bool currentHoverState = false;

    public void Start()
    {
        transform.position = startPosition;
        transform.rotation = Quaternion.Euler(startRotation);
    }

    public void OnIsHovering(bool isHovering)
    {
        // 1. If we are already moving, don't interrupt!
        if (isTweening) return;

        // 2. If the state hasn't actually changed, do nothing
        if (isHovering == currentHoverState) return;

        currentHoverState = isHovering;
        ExecuteTween(isHovering);
    }

    private void ExecuteTween(bool isHovering)
    {
        Vector3 destPos = isHovering ? hoverPosition : startPosition;
        Vector3 destRot = isHovering ? hoverRotation : startRotation;

        isTweening = true;

        // Use a Sequence to group position and rotation
        Sequence s = DOTween.Sequence();

        s.Join(transform.DOMove(destPos, tweenSpeed).SetEase(easeType));
        s.Join(transform.DORotate(destRot, tweenSpeed).SetEase(easeType));

        // 3. When the animation is 100% done, allow new inputs
        s.OnComplete(() => {
            isTweening = false;
            
            // Check if the mouse is still there (or gone) to prevent getting stuck
            // This acts as a safety check in case the mouse moved during the tween
            // (You may need a way to check current mouse status here)
        });
    }
}