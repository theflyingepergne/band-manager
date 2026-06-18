using UnityEngine;
using UnityEngine.InputSystem;


public class QuitGameOnKeypress : MonoBehaviour
{		
	void Update()
	{
		if (Keyboard.current.escapeKey.wasPressedThisFrame)
		{
			Application.Quit();
		}
	}
}