using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUIManager : Singleton<MainMenuUIManager>
{
    public async void OnPlayButtonClicked()
    {
        await CameraFade.Instance.DoCameraFade(1);
        // await Task.Delay(200);

        // Eventually play actual game but for now load ViewBandMember scene
        SceneManager.LoadScene("ViewBandMembers");
    }

    public void OnSettingsButtonClicked()
    {
        // Implement settings functionality here
        Debug.Log("Settings button clicked!");
    }

    public void OnQuitButtonClicked()
    {
        Application.Quit();
    }
}
