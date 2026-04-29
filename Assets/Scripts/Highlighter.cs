// Add this script to objects that you want to highlight when mousing over
using UnityEngine;

public class Highlighter : MonoBehaviour, IHoverable
{
    Color originalColour;
    [SerializeField] Color highlightedColour = new Color(255f, 0f, 0f, 175f);
    SpriteRenderer spriteRenderer;

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
            spriteRenderer.material.color = isHovering ? highlightedColour : originalColour;
        }
    }
}