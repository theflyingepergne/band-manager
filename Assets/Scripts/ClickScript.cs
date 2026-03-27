using UnityEngine;
using UnityEngine.InputSystem;

public class ClickScript : MonoBehaviour
{
    private Collider2D lastHitCollider;

    void Update()
    {
        // Get mouse position
        Vector2 mousePos = Mouse.current.position.ReadValue();
        
        // Perform Raycast
        RaycastHit2D hit2D = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(mousePos), Vector2.zero);
        Collider2D current = hit2D.collider;

        // Are we hovering on anything that implements IHighlighter
        // If what we are hovering over != lastHitCollider
        // Highlight current and stop highlighting lastHitCollider
        if (current != lastHitCollider)
        {
            lastHitCollider?.GetComponent<IHighlightable>()?.OnIsHovering(false);
            current?.GetComponent<IHighlightable>()?.OnIsHovering(true);
            lastHitCollider = current;
        }

        // Did we click on anything
        if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            if (current != null)
            {
                current.GetComponent<IClickable>()?.OnClicked();
            }
            else
            {
                // Reference the Singleton directly here
                ViewBandMemberUIManager.Instance?.ShowBandMemberDetails(false);
            }
        }
    }
}