using UnityEngine;
using UnityEngine.InputSystem;

public class ClickScript : MonoBehaviour
{
    InputAction clickAction;
    GameObject selectedObject;

    public void Start()
    {
        clickAction = InputSystem.actions.FindAction("Click");
    }
        
}

public void Update()
{
    
}

    
