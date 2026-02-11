using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MainMenuUIManager : Singleton<MainMenuUIManager>
{
    [SerializeField] private UIDocument mainMenuUI;
    private Button playButton;
    private Button settingsButton;
    private Button quitButton;

    private void Start()
    {
        var root = mainMenuUI.rootVisualElement;

        // Hooking up buttons using strings...
        playButton = mainMenuUI.rootVisualElement.Q<Button>("PlayButton");
        settingsButton = mainMenuUI.rootVisualElement.Q<Button>("SettingsButton");
        quitButton = mainMenuUI.rootVisualElement.Q<Button>("QuitButton");

        playButton.clicked += OnPlayButtonClicked;
        settingsButton.clicked += OnSettingsButtonClicked;
        quitButton.clicked += OnQuitButtonClicked;
    }

    private void OnPlayButtonClicked()
    {
        // Eventually play actual game but for now load ViewBandMember scene
        SceneManager.LoadScene("ViewBandMember");
    }

    private void OnSettingsButtonClicked()
    {
        // Implement settings functionality here
        Debug.Log("Settings button clicked!");
    }

    private void OnQuitButtonClicked()
    {
        Application.Quit();
    }
}
