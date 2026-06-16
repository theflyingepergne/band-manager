// Add this script to objects that you want to highlight when mousing over
using UnityEngine;
using DG.Tweening;

public class Highlighter : MonoBehaviour, IHoverable
{
    //---References---//
    [SerializeField] private Color32 highlightedColour = new Color32(191, 0, 0, 175);
    [SerializeField] private float duration = 0.1f;

    //---Local References---//
    private Color originalColour;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColour = spriteRenderer.material.color;
    }

    public void OnIsHovering(bool isHovering)
    {
        if (spriteRenderer != null)
        {
            // If hovering, use highlighted colour, otherwise use original colour
            if (isHovering)
            {
                spriteRenderer.material.DOColor(highlightedColour, duration);
            }
            else
            {
                spriteRenderer.material.DOColor(originalColour, duration);
            }
        }
    }
}