using UnityEngine;
using UnityEngine.InputSystem;

public class ClickScript : MonoBehaviour
{
    InputAction clickAction;
    Collider2D lastHitCollider;

    void Start()
    {
        clickAction = InputSystem.actions.FindAction("Click");
    }

    void Update()
    {
        // Convert mouse position to world coordinates and perform a raycast
        Vector2 mousePos = Mouse.current.position.ReadValue();
        RaycastHit2D hit2D = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(mousePos), Vector2.zero);

        // If last collider != current collider, highlight the collider
        Collider2D current = hit2D.collider;
        if (current != lastHitCollider)
        {
            var lastClickable = lastHitCollider?.GetComponent<IHighlightable>();
            lastClickable?.OnIsHovering(false);

            var currentClickable = current?.GetComponent<IHighlightable>();
            currentClickable?.OnIsHovering(true);

            lastHitCollider = current;
        }

        // If we click on a collider, do something
        // If we click on empty space, hide the band member details
        if (clickAction.WasReleasedThisFrame())
        {
            if (current != null)
            {
                var clickable = current.GetComponent<IClickable>();
                clickable?.OnClicked();
            }
            else
            {
                // Clicked on empty space, hide the band member details panel
                ViewBandMemberUIManager.Instance.ShowBandMemberDetails(false, null);
            }
        }
    }
}

