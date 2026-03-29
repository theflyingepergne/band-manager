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

        ProcessHoveringOverSomething(current);
        ProcessClickingOnSomething(current);
    }

    private static void ProcessClickingOnSomething(Collider2D current)
    {
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
                ViewBandMembersUIManager.Instance?.ShowBandMemberDetails(false);
            }
        }
    }

    private void ProcessHoveringOverSomething(Collider2D current)
    {
        // Are we hovering on anything that implements IHighlighter
        // If what we are hovering over != lastHitCollider
        // Highlight current and stop highlighting lastHitCollider
        if (current != lastHitCollider)
        {
            lastHitCollider?.GetComponent<IHoverable>()?.OnIsHovering(false);
            current?.GetComponent<IHoverable>()?.OnIsHovering(true);
            lastHitCollider = current;
        }
    }
}