// Add this script to objects that you want to highlight when mousing over
using UnityEngine;

public class Highlighter : MonoBehaviour, IHighlightable
{
    Color originalColour;
    [SerializeField] Color highlightedColour = new Color(255f, 0f, 0f, 125f);
    SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColour = spriteRenderer.material.color;
    }

    public void OnIsHovering(bool isHovering)
    {
        // If hovering, change the colour to the highlighted colour, otherwise change it back to the original colour
        if (isHovering)
        {
            spriteRenderer.material.color = highlightedColour;
        }
        else
        {
            spriteRenderer.material.color = originalColour;
        }
    }
}
