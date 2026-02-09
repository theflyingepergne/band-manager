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

        // If we released the click action this frame, check if we hit something
        if (clickAction.WasReleasedThisFrame())
        {
            if ( hit2D.collider != null)
            {
                Debug.Log("Mouse Clicked: " + hit2D.collider.gameObject.name + " at position: " + mousePos);    
            }
            else
            {
                Debug.Log("Hit nothing at: " + mousePos);
            }
            
        }
    }
}

