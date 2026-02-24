using UnityEngine;
using UnityEngine.InputSystem;

public class ClickScript : MonoBehaviour
{
    InputAction clickAction;

    void Start()
    {
        clickAction = InputSystem.actions.FindAction("Click");
    }

    void Update()
    {
        // Get mouse position in screen coordinates
        Vector2 mousePos = Mouse.current.position.ReadValue();

        // Convert mouse position to world coordinates and perform a raycast
        RaycastHit2D hit2D = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(mousePos), Vector2.zero);

        if(hit2D.collider != null && hit2D.collider.TryGetComponent<IClickable>(out IClickable clickable))
        {
            OnAttemptClick(hit2D);
            OnAttemptHover(hit2D);
        }
    }

    private void OnAttemptClick(RaycastHit2D hit2D)
    {
        // If we released the click action this frame, check if we hit something
        if (clickAction.WasReleasedThisFrame())
        {
            hit2D.collider.GetComponent<IClickable>().OnClicked();
        }
        else
        {
            // If we didn't hit anything, hide the band member details panel
            // Debug.Log("No collider hit, hiding details panel");
            ViewBandMemberUIManager.Instance.ShowBandMemberDetails(false);
        }
    }

    private void OnAttemptHover(RaycastHit2D hit2D)
    {
        if (hit2D.collider != null)
        {
            IClickable clickable = hit2D.collider.GetComponent<IClickable>();
            clickable.OnIsHovering(true);
        }
    }
}

