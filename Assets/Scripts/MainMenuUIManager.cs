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
        root.style.width = new StyleLength(new Length(100, LengthUnit.Percent));
        root.style.height = new StyleLength(new Length(100, LengthUnit.Percent));
        
        playButton = mainMenuUI.rootVisualElement.Q<Button>("PlayButton");
        settingsButton = mainMenuUI.rootVisualElement.Q<Button>("SettingsButton");
        quitButton = mainMenuUI.rootVisualElement.Q<Button>("QuitButton");

        playButton.clicked += OnPlayButtonClicked;
        settingsButton.clicked += OnSettingsButtonClicked;
        quitButton.clicked += OnQuitButtonClicked;
    }

    private void OnPlayButtonClicked()
    {
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
