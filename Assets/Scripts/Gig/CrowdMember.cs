using UnityEngine;
using DG.Tweening;

public class CrowdMember : MonoBehaviour
{
    [Header("Sprites")]
    [SerializeField] private Sprite[] crowdSprites; // Array of different crowd member sprites
    // Array indices:
    // 0 = closest to camera
    // 1 = middle
    // 2 = furthest from camera

    [Header("Jumping Settings")]
    [SerializeField] private float jumpForce = 0.2f;
    [SerializeField] private float duration = 0.25f;

    public void AssignSprite(int index)
    {
        if (crowdSprites != null)
        {
            SpriteRenderer sr = GetComponent<SpriteRenderer>();
            // Assign sprite based on index passed from CrowdManager
            sr.sprite = crowdSprites[index];

        }
    }

    public void StartJumping()
    {
        // Add a random delay so they don't all jump at the exact same millisecond
        float randomDelay = Random.Range(0f, 0.5f);

        // This creates a looping "Punch" effect
        transform.DOPunchPosition(new Vector3(0, jumpForce, 0), duration, 1, 0)
            .SetDelay(randomDelay)
            .SetLoops(-1, LoopType.Restart);
    }

    private void OnDestroy()
    {
        StopJumping();
    }

    public void StopJumping()
    {
        transform.DOKill(); // Stop the tween
        // transform.DOLocalMoveY(0, 0.2f); // Snap back to ground
    }
}
