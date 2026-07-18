using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class CalendarManager : MonoBehaviour, IPointerClickHandler
{
    //---References---//
    [Header("UI References")]
    [SerializeField] private RectTransform calendarBorder;
    [SerializeField] private RectTransform gridContainer;
    [SerializeField] private TMP_Text monthText;
    [SerializeField] private TMP_Text yearText;
    [SerializeField] private GameObject BG;
    [SerializeField] private RectTransform notesContainer;

    [Header("Prefab references")]
    [SerializeField] private GameObject calendarDayPrefab;
    [SerializeField] private GameObject calendarNotePrefab;

    [Header("Scriptable Objects")]
    [SerializeField] private GameEventData bookGigEventData;

    [Header("Notes Tween controls")]
    [SerializeField] private float strengthX = 1.4f;
    [SerializeField] private float strengthY = 1.4f;
    [SerializeField] private float duration = 0.4f;
    [SerializeField] private int vibrato = 1;
    [SerializeField] private int elasticity = 1;

    [Header("Date")]
    public GameDate date = new(1, 1, 1979);
    
    //---Local References---//
    private DateManager dm;
    private ScheduleManager sm;
    private Tween notesTween;
    private List<ScheduledEvent> scheduledEvents;
    private GameDate lastInspectedDate;

    //---Events---//
    public static System.Action<bool> OnChangeCalendarVisibility;

    //---Init Methods---//
    private void OnEnable()
    {
        dm = DateManager.Instance;
        sm = ScheduleManager.Instance;

        DateManager.OnDateChanged += HandleDateChanged;
        CalendarDay.OnInspectDay += HandleInspectDay;
        DialogueManager.OnDialogueEventTriggered += HandleDialogueEvent;
    }

    private void OnDisable()
    {
        DateManager.OnDateChanged -= HandleDateChanged;
        CalendarDay.OnInspectDay -= HandleInspectDay;
        DialogueManager.OnDialogueEventTriggered -= HandleDialogueEvent;

        ClearNotes();
    }

    //---Visibility---//
    public void OpenCalendar()
    {
        GetComponent<Canvas>().enabled = true;
        OnChangeCalendarVisibility?.Invoke(true);
        SetupCalendar();
    }

    public void CloseCalendar()
    {
        GetComponent<Canvas>().enabled = false;
        OnChangeCalendarVisibility?.Invoke(false);
    }

    private void SetupCalendar()
    {
        date = dm.date;
        scheduledEvents = sm.scheduledEvents;

        ClearCalendar();
        ClearNotes();

        // For however many days there are in the current month...
        for (int d = 1; d < MonthList.Months[date.month].Days + 1; d++)
        {
            // Create CalendarDay prefabs and parent to Calendar gridContainer
            GameObject newCalendarDay = Instantiate(calendarDayPrefab, gridContainer, false);
            CalendarDay calendarDay = newCalendarDay.GetComponent<CalendarDay>();

            // Initialize empty list of scheduledEvents to populate with eventsToAdd
            List<ScheduledEvent> eventsToAdd = new();

            // For each scheduledEvents in list of scheduledEvents (scheduledEventsbase),
            // If the date of the GameEvent matches the date of the current iteration
            // Add it to a list of eventsToAdd
            foreach (ScheduledEvent e in scheduledEvents)
            {
                if (e.date.day == d && e.date.month == date.month && e.date.year == date.year && (!e.onlyShowOnCompleted || e.isCompleted))
                {
                    eventsToAdd.Add(e);
                }
            }

            // Populate CalendarDay with eventsToAdd
            calendarDay.SetupDay(new GameDate(d, date.month, date.year), eventsToAdd);

            // When we create the CalendarDay which has the current date...
            if (calendarDay.localDate.IsSameDate(date))
            {
                // Use its eventsToAdd to populate the notes
                HandleInspectDay(calendarDay, eventsToAdd);
            }
        }

        // Set Month and Year text
        monthText.text = MonthList.Months[date.month].Name;
        yearText.text = date.year.ToString();
    }

    private void ClearCalendar()
    {
        for (int i = gridContainer.childCount - 1; i >= 0; i--)
        {
            Transform child = gridContainer.GetChild(i);
            child.SetParent(null);
            Destroy(child.gameObject);
        }
    }

    //---Calendar Events---//
    private void HandleDateChanged(GameDate updatedDate)
    {
        date = updatedDate;
        SetupCalendar();
    }

    private void HandleInspectDay(CalendarDay calendarDay, List<ScheduledEvent> eventsToAdd)
    {
        lastInspectedDate = calendarDay.localDate;

        if (eventsToAdd.Count > 0)
        {
            ClearNotes();

            foreach (ScheduledEvent e in eventsToAdd)
            {
                // Create calendarNote
                GameObject newCalendarNotePrefab = Instantiate(calendarNotePrefab, notesContainer, false);
                TMP_Text newCalendarNoteTMP_Text = newCalendarNotePrefab.GetComponentInChildren<TMP_Text>();

                SetFontStyle(e, newCalendarNoteTMP_Text);

                // Set note text
                newCalendarNoteTMP_Text.text = "- " + e.title;

                // Setup tween using TextMeshPro transform rather than prefab transform
                // SetupTween(newCalendarNoteTMP_Text);

            }
        }
    }

    //---Cosmetic---//
    private void SetFontStyle(ScheduledEvent e, TMP_Text t)
    {
        // If date is in the past OR complete, strikethrough text
        if (e.date.IsBeforeDate(date) || e.isCompleted)
        {
            t.fontStyle = FontStyles.Strikethrough | FontStyles.Bold;
        }
        else
        {
            t.fontStyle = FontStyles.Bold;
        }
    }

    private void SetupTween(TMP_Text text)
    {
        notesTween = text.transform.DOPunchScale
            (
                new Vector2(1f * strengthX, 1f * strengthY),
                duration,
                vibrato,
                elasticity
            );
    }

    //---Notes---//
    private void ClearNotes()
    {
        if (notesContainer.childCount > 0)
        {
            KillNotesTweens();

            for (int i = notesContainer.childCount - 1; i >= 0; i--)
            {
                Transform child = notesContainer.GetChild(i);
                child.SetParent(null);
                Destroy(child.gameObject);
            }
        }
    }

    private void KillNotesTweens()
    {
        if (notesTween.IsActive())
        {
            notesTween.Kill();
            notesTween = null;
        }
    }

    //---Input---//
    public void OnPointerClick(PointerEventData eventData)
    {
        // close calendar if we click off of it
        if (eventData.pointerCurrentRaycast.gameObject == BG)
        {
            CloseCalendar();
        }
    }

    //---Dialogue Events---//
    public void HandleDialogueEvent(string eventName, string eventParameter)
    {
        switch (eventName)
        {
            case "start_booking_gig":
                OpenCalendar();
                break;
            
            case "accept_booking":
                VenueData venue = BandManager.Instance.destinationVenue;

                ScheduleManager.Instance.ScheduleNewEvent(bookGigEventData, lastInspectedDate, $"Gig at {venue.name}");
                ScheduleManager.Instance.ScheduleNewGig(venue, lastInspectedDate);
                break;
            
            default:
                return;
        }
    }
}