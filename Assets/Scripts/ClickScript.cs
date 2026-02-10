using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class ClickScript : MonoBehaviour
{
    InputAction clickAction;

    void Start()
    {
        clickAction = InputSystem.actions.FindAction("Click");
    }

    void Update() // see if we clicked on something that implements IClickable
    {
        // Get mouse position in screen coordinates
        Vector2 mousePos = Mouse.current.position.ReadValue();

        // Convert mouse position to world coordinates and perform a raycast
        RaycastHit2D hit2D = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(mousePos), Vector2.zero);

        // If we released the click action this frame, check if we hit something
        if (clickAction.WasReleasedThisFrame())
        {

            if (hit2D.collider != null && hit2D.collider.TryGetComponent<IClickable>(out IClickable clickable))
            {
                // If the hit object has a collider and an IClickable component, call its OnClicked method
                clickable.OnClicked();

            }
            else
            {
                // If we didn't hit anything, hide the band member details panel
                // Debug.Log("No collider hit, hiding details panel");
                ViewBandMemberUIManager.Instance.ShowBandMemberDetails(false);
            }

        }

    }
}

