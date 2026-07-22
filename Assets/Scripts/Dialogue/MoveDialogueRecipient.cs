using UnityEngine;
using DG.Tweening;

public class MoveDialogueRecipient : MonoBehaviour
{
    //---References---//
    [Header("Anchors (Local Positions)")]
    [SerializeField] private Transform anchorOffscreen;
    [SerializeField] private Transform anchorTalking;

    [Header("Target Sprite")]
    [SerializeField] private Transform spriteTransform;

    private void Awake()
    {
        // Default the sprite to the offscreen anchor immediately before dialogue starts
        if (spriteTransform != null && anchorOffscreen != null)
        {
            spriteTransform.localPosition = anchorOffscreen.localPosition;
        }
    }

    // Tweens the sprite to the talking anchor. Returns a Tween so sequences can append it.
    public Tween MoveToTalkingAnchor(float duration)
    {
        if (spriteTransform == null || anchorTalking == null) return null;

        return spriteTransform.DOLocalMove(anchorTalking.localPosition, duration)
            .SetEase(Ease.OutBack);
    }

    // Tweens the sprite back offscreen.
    public Tween MoveToOffscreenAnchor(float duration)
    {
        if (spriteTransform == null || anchorOffscreen == null) return null;

        return spriteTransform.DOLocalMove(anchorOffscreen.localPosition, duration)
            .SetEase(Ease.InBack);
    }
}