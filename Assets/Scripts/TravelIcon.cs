using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TravelIcon : MonoBehaviour, IClickable
{
    [SerializeField] private string sceneToLoad = "Map";
    [SerializeField] private bool doCameraFade = true;

    public async void OnClicked()
    {
        if (doCameraFade == true)
        {
            await CameraFade.Instance.DoCameraFade(1);
            // await Task.Delay(200);
        }
        // Debug.Log("Travel icon clicked");
        SceneManager.LoadScene(sceneToLoad);
    }
}
