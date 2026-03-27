using UnityEngine;
using UnityEngine.SceneManagement;

public class TravelIcon : MonoBehaviour, IClickable
{
    [SerializeField] private string sceneToLoad = "Map";
    public void OnClicked()
    {
        // Debug.Log("Travel icon clicked");
        SceneManager.LoadScene(sceneToLoad);
    }
}
