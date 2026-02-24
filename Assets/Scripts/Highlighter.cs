// Add this script to objects that you want to highlight when mousing over
using UnityEngine;

public class Highlighter : MonoBehaviour, IClickable
{
    Color originalColour;
    [SerializeField] Color highlightedColour = new Color(255, 0, 0);
    SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            Debug.Log("found sprite renderer.");
        }

        originalColour = spriteRenderer.material.color;
        if (originalColour != null)
        {
            Debug.Log("found material.");
        }
    }

    public void OnIsHovering(bool isHovering)
    {
        spriteRenderer.material.color = isHovering ? highlightedColour : originalColour;
    }

    public void OnClicked()
    {
        Debug.Log("Clicked venue icon.");
    }

}
