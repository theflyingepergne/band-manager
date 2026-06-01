using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class ClickScript : MonoBehaviour
{
    //---References---//
    private Collider2D lastHitCollider;

    //---Events---//
    public static System.Action OnClickEmptySpace; // empty space as in no Collider2D

    void Update()
    {
        // Get mouse position
        Vector2 mousePos = Mouse.current.position.ReadValue();

        // Check if the mouse is currently hovering over a UI element
        if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
        {
            // The mouse is over UI! Stop here so we don't highlight or click sprites behind it.
            return;
        }

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
                // Tell anyone who is listening that we clicked on empty space
                OnClickEmptySpace?.Invoke();
            }
        }
    }

    private void ProcessHoveringOverSomething(Collider2D current)
    {
        // Debug.Log("Hovering on " + current);
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