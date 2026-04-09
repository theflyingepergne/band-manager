using UnityEngine;
using DG.Tweening;

public class PrepareSetlistManager : Singleton<PrepareSetlistManager>, IHoverable
{
    [Header("UI")]
    [SerializeField] private RectTransform setlistWrapper;

    [Header("Positions")]
    [SerializeField] private Transform startAnchor;
    [SerializeField] private Transform hoverAnchor;

    [Header("Animation")]
    [SerializeField] private float tweenSpeed = 0.5f;
    [SerializeField] private Ease easeType = Ease.OutBack;

    private bool isTweening = false;
    private bool currentHoverState = false;

    public void Start()
    {
        transform.position = startAnchor.position;
        transform.rotation = startAnchor.rotation;

        // TODO instantiate prefabs with some kind of Setup() script
        // TODO add prefabs to setlistWrapper vertical layout group
    }

    public void OnIsHovering(bool isHovering)
    {
        // If we are already tween, don't interrupt tween
        if (isTweening) return;

        //  If the state hasn't actually changed, do nothing
        if (isHovering == currentHoverState) return;

        currentHoverState = isHovering;
        ExecuteTween(isHovering);
    }

    private void ExecuteTween(bool isHovering)
    {
        // Use the position and rotation of the anchor objects as target pos & rot
        Vector3 targetPos = isHovering ? hoverAnchor.position : startAnchor.position;
        Vector3 targetRot = isHovering ? hoverAnchor.eulerAngles : startAnchor.eulerAngles;

        isTweening = true;
        Sequence seq = DOTween.Sequence();

        seq.Join(transform.DOMove(targetPos, tweenSpeed).SetEase(easeType));
        seq.Join(transform.DORotate(targetRot, tweenSpeed).SetEase(easeType));

        seq.OnComplete(() => isTweening = false);
    }

    private int currentHoveredIndex = -1;

    public void SetCurrentHoverIndex(int index)
    {
        currentHoveredIndex = index;
    }

    public int GetCurrentHoverIndex() => currentHoveredIndex;
}