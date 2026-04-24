using TMPro;
using UnityEngine;

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

    //---Methods---//
    void Start()
    {
        // Setup event with default data
        SetupEvent(gameEventData);
    }

    public void SetupEvent(GameEventData data)
    {
        if (data != null)
        {
            gameEventData = data;
        }
        eventTitle.text = gameEventData.title;
        eventDescription.text = gameEventData.description;
        eventSprite.GetComponent<SpriteRenderer>().sprite = gameEventData.sprite;
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
