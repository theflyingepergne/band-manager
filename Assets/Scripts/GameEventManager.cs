using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameEventManager : MonoBehaviour
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
        ClearEventChoiceButtons();

        foreach (EventChoice choice in gameEventData.choices)
        {
            GameObject eCBP = Instantiate(eventChoiceButtonPrefab, eventChoiceWrapper, false);
            eCBP.GetComponentInChildren<TextMeshProUGUI>().text = choice.choiceLabel;
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

    public void OnChoiceSelected(EventChoice chosen)
    {
        // Perform any stat changes
        if (chosen.StatChanges.Count > 0)
        {
            foreach (var statChange in chosen.StatChanges)
            {
                statChange.ApplyStatChange();
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

        // Hide the UI and clear the phone notification
        // CloseEventWindow();
    }
}
