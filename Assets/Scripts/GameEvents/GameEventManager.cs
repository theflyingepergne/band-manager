using System.Collections.Generic;
using System.Text;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class GameEventManager : Singleton<GameEventManager>
{
    //---References---//
    [Header("Data")]
    [SerializeField] private ScheduledEvent scheduledEvent;

    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI eventTitle;
    [SerializeField] private TextMeshProUGUI eventDescription;
    [SerializeField] private GameObject eventSprite;
    [SerializeField] private RectTransform eventChoiceWrapper;
    [SerializeField] private RectTransform notificationsPanel;
    [SerializeField] private TypewriterEffect typewriter;

    [Header("Prefabs")]
    [SerializeField] private GameObject gameEventPrefab;
    [SerializeField] private GameObject eventChoiceButtonPrefab;
    [SerializeField] private GameObject eventNotificationPrefab;

    [Header("Tween controls")]
    [SerializeField] private float strength = 1.4f;
    [SerializeField] private float duration = 0.4f;
    [SerializeField] private int vibrato = 1;
    [SerializeField] private int elasticity = 1;

    //---Local References---//
    private bool selectedChoice = false;

    //---Events---//
    private readonly List<System.Action> pendingStatChange = new();
    private readonly List<System.Action> pendingCustomGameEvents = new();

    private void OnEnable()
    {
        CameraFade.OnFadeInComplete += HandleFadeInComplete;
        CameraFade.OnFadeOutComplete += HandleFadeOutComplete;
    }

    private void OnDisable()
    {
        CameraFade.OnFadeInComplete -= HandleFadeInComplete;
        CameraFade.OnFadeOutComplete -= HandleFadeOutComplete;
    }

    //---Local References---//
    ScheduleManager sm;

    //---Methods---//
    private void Start()
    {
        sm = ScheduleManager.Instance;
    }

    //---Called by clicking on event notification---//
    public void OnTriggerGameEvent(ScheduledEvent data)
    {
        if (gameEventPrefab != null)
        {
            selectedChoice = false;
            gameEventPrefab.SetActive(true);
            SetupEvent(data);
        }
    }

    public void CloseEventWindow()
    {
        typewriter.CompleteTextRevealed -= HandleCompleteTextRevealed;
        gameEventPrefab.SetActive(false);
    }

    public void SetupEvent(ScheduledEvent data)
    {
        scheduledEvent = data;

        // Setup title, description and sprite
        eventTitle.text = scheduledEvent.title;
        eventDescription.text = scheduledEvent.gameEventData.description;
        eventSprite.GetComponent<SpriteRenderer>().sprite = scheduledEvent.gameEventData.sprite;

        ClearEventChoiceButtons();

        typewriter.CompleteTextRevealed += HandleCompleteTextRevealed;
        typewriter.StartTypewriter(eventDescription.text);
    }

    private void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            if (typewriter != null)
            {
                // If typewriter is typing, the click should pass down to the typewriter to speed up/skip
                if (typewriter.IsTyping)
                {
                    typewriter.Skip(true);
                    return;
                }
            }
        }
    }

    private void HandleCompleteTextRevealed()
    {
        // if seenChoices == false {show choices}
        // else {show close button}
        if (!selectedChoice)
        {
            // if there are choices to show after typing description
            SetupEventChoiceButtons();
        }
        else
        {
            ProcessStatChanges();
            ProcessCustomGameEvents();
            CreateCloseButton();
        }
    }

    private void ProcessStatChanges()
    {
        foreach (var statChange in pendingStatChange)
        {
            statChange?.Invoke();
        }
        pendingStatChange.Clear();

    }

    private void ProcessCustomGameEvents()
    {
        foreach (var customGameEvent in pendingCustomGameEvents)
        {
            customGameEvent?.Invoke();
        }
        pendingCustomGameEvents.Clear();
    }

    private void SetupEventChoiceButtons()
    {
        // Clear any old buttons
        ClearEventChoiceButtons();

        // Create a button for each choice
        foreach (EventChoice choice in scheduledEvent.gameEventData.choices)
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
        selectedChoice = true;
        // Prepare text player will see after picking a choice
        StringBuilder sb = new();
        sb.AppendLine($"{chosen.choiceOutcomeDescription}\n");

        if (chosen.StatChanges.Count > 0)
        {
            foreach (var statChange in chosen.StatChanges)
            {
                // Queue up any stat changes
                pendingStatChange.Add(statChange.ApplyStatChange);

                // Add stat changes to description
                sb.AppendLine(statChange.GetStatText());
            }
        }

        if (chosen.CustomEvents.Count > 0)
        {
            foreach (var customEvent in chosen.CustomEvents)
            {
                // Queue up any custom game events
                pendingCustomGameEvents.Add(customEvent.Execute);
            }
        }

        ClearEventChoiceButtons();

        // Set event description to chosen EventChoice outcome
        // as well as any stat changes
        eventDescription.text = sb.ToString();
        typewriter.StartTypewriter(eventDescription.text);
    }

    private void CreateCloseButton()
    {
        // Create button
        GameObject c = Instantiate(eventChoiceButtonPrefab, eventChoiceWrapper, false);
        c.GetComponentInChildren<TextMeshProUGUI>().text = "[Close]";

        // Close window on click
        Button btn = c.GetComponentInChildren<Button>();
        btn.onClick.AddListener(() => CloseEventWindow());
    }

    //---Event Notifications---//
    private void HandleFadeOutComplete()
    {
        ClearNotifications();
    }

    private void HandleFadeInComplete()
    {
        CreateEventNotifications();
    }

    private void CreateEventNotifications()
    {
        ClearNotifications();

        // Initialize sequence
        Sequence notificationSequence = DOTween.Sequence();

        foreach (ScheduledEvent scheduledEvent in sm.GetTodaysEvents())
        {
            // Create & setup event notification
            GameObject newEventNotificationPrefab = Instantiate(eventNotificationPrefab, notificationsPanel, false);
            EventNotification newEventNotification = newEventNotificationPrefab.GetComponent<EventNotification>();
            newEventNotification.SetupEventNotification(scheduledEvent);
            // Debug.Log($"created event: {eventData.title}");

            // Append tween to sequence
            notificationSequence.Append
            (
                newEventNotificationPrefab.transform.DOPunchScale(Vector2.one * strength,
                duration,
                vibrato,
                elasticity)
                .OnStart(newEventNotification.ShowNotification)
            );
        }

        // Play sequence
        notificationSequence.Play();
    }

    private void ClearNotifications()
    {
        for (int i = notificationsPanel.childCount - 1; i >= 0; i--)
        {
            Transform child = notificationsPanel.GetChild(i);
            child.SetParent(null);
            Destroy(child.gameObject);
        }
    }
}