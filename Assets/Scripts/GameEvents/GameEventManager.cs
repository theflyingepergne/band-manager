using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameEventManager : Singleton<GameEventManager>
{
    //---References---//
    [Header("Data")]
    [SerializeField] private GameEventData gameEventData;
    [SerializeField] private GameEventDatabase gameEventDatabase;

    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI eventTitle;
    [SerializeField] private TextMeshProUGUI eventDescription;
    [SerializeField] private GameObject eventSprite;
    [SerializeField] private RectTransform eventChoiceWrapper;

    [Header("Prefabs")]
    [SerializeField] private GameObject eventChoiceButtonPrefab;

    //---Methods---//
    //---Setup before picking choice---//
    void Start()
    {
        SetupEvent(gameEventData);
    }

    public void SetupEvent(GameEventData data)
    {
        // If no data was passed in, use default gameEventData
        if (data != null)
        {
            gameEventData = data;
        }

        // Setup title, description and sprite
        eventTitle.text = gameEventData.title;
        eventDescription.text = gameEventData.description;
        eventSprite.GetComponent<SpriteRenderer>().sprite = gameEventData.sprite;

        SetupEventChoiceButtons();
    }

    private void SetupEventChoiceButtons()
    {
        // Clear any old buttons
        ClearEventChoiceButtons();

        // Create a button for each choice
        foreach (EventChoice choice in gameEventData.choices)
        {
            GameObject eCBP = Instantiate(eventChoiceButtonPrefab, eventChoiceWrapper, false);
            eCBP.GetComponentInChildren<TextMeshProUGUI>().text = choice.choiceLabel;

            // Call OnChoiceSelected on button click
            Button btn = eCBP.GetComponent<Button>();
            btn.onClick.AddListener(() => OnChoiceSelected(choice));
        }
    }

    private void ClearEventChoiceButtons()
    {
        // Clear any existing buttons
        for (int i = eventChoiceWrapper.childCount - 1; i >= 0; i--)
        {
            Transform child = eventChoiceWrapper.GetChild(i);
            child.SetParent(null);
            Destroy(child.gameObject);
        }
    }

    //---Setup after picking choice---//
    public void OnChoiceSelected(EventChoice chosen)
    {
        // Prepare text player will see after picking a choice
        StringBuilder sb = new StringBuilder();
        sb.AppendLine($"{chosen.choiceOutcomeDescription}\n");

        // Perform any stat changes
        if (chosen.StatChanges.Count > 0)
        {
            foreach (var statChange in chosen.StatChanges)
            {
                statChange.ApplyStatChange();
                sb.AppendLine(statChange.GetStatText());
            }
        }

        // Run any custom game events
        if (chosen.CustomEvents.Count > 0)
        {
            foreach (var customEvent in chosen.CustomEvents)
            {
                customEvent.Execute();
            }
        }

        // Clear EventChoiceButtons
        ClearEventChoiceButtons();

        // Set event description to chosen EventChoice outcome
        // as well as any stat changes
        eventDescription.text = sb.ToString();

        // Create close button and add it to wrapper
        CreateCloseButton();
    }

    private void CreateCloseButton()
    {
        // Create button
        GameObject c = Instantiate(eventChoiceButtonPrefab, eventChoiceWrapper, false);
        c.GetComponentInChildren<TextMeshProUGUI>().text = "Close";

        // Close window on click
        Button btn = c.GetComponentInChildren<Button>();
        btn.onClick.AddListener(() => CloseEventWindow());
    }

    public void CloseEventWindow()
    {
        gameObject.SetActive(false);
    }
}