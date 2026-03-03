using UnityEngine;
using UnityEngine.SceneManagement;

public class TravelIcon : MonoBehaviour, IClickable
{
    public void OnClicked()
    {
        Debug.Log("Travel icon clicked");
        SceneManager.LoadScene("Map");
    }
}
