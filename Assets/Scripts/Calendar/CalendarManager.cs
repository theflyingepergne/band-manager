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

    [Header("Notes Tween controls")]
    [SerializeField] private float strengthX = 1.4f;
    [SerializeField] private float strengthY = 1.4f;
    [SerializeField] private float duration = 0.4f;
    [SerializeField] private int vibrato = 1;
    [SerializeField] private int elasticity = 1;

    public GameDate date = new(1, 1, 1979);

    //---Local References---//
    DateManager dm;
    ScheduleManager sm;
    Tween notesTween;
    List<ScheduledEvent> scheduledEvents;

    //---Methods---//
    private void OnEnable()
    {
        dm = DateManager.Instance;
        sm = ScheduleManager.Instance;

        DateManager.OnDateChanged += HandleDateChanged;
        CalendarDay.OnInspectDay += HandleInspectDay;

        SetupCalendar();
    }

    private void OnDisable()
    {
        DateManager.OnDateChanged -= HandleDateChanged;
        CalendarDay.OnInspectDay -= HandleInspectDay;
        
        ClearNotes();
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
                if (e.date.day == d && e.date.month == date.month && e.date.year == date.year)
                {
                    eventsToAdd.Add(e);
                }
            }

            // Populate CalendarDay with eventsToAdd
            calendarDay.SetupDay(new GameDate(d, date.month, date.year), eventsToAdd);

            // When we create the CalendarDay which has the current date...
            if (calendarDay.date.isSameDate(date))
            {
                // Use its eventTextBlock to populate the notes
                HandleInspectDay(eventsToAdd);
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

    private void HandleDateChanged(GameDate updatedDate)
    {
        date = updatedDate;
        SetupCalendar();
    }

    private void HandleInspectDay(List<ScheduledEvent> eventsToAdd)
    {
        ClearNotes();

        // Instantiate CalendarNote
        foreach (ScheduledEvent e in eventsToAdd)
        {
            GameObject newCalendarNotePrefab = Instantiate(calendarNotePrefab, notesContainer, false);
            TMP_Text newCalendarNoteTMP_Text = newCalendarNotePrefab.GetComponentInChildren<TMP_Text>();

            SetFontStyle(e, newCalendarNoteTMP_Text);
            
            // Set note text
            newCalendarNoteTMP_Text.text = "- " + e.gameEventData.title;

            // Setup tween using TextMeshPro transform rather than prefab transform
            SetupTween(newCalendarNoteTMP_Text);

        }
    }

    private void SetFontStyle(ScheduledEvent e, TMP_Text t)
    {
        // If date is in the past OR complete, strikethrough text
        if (e.date.isBeforeDate(date) || e.isCompleted)
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

    public void OnPointerClick(PointerEventData eventData)
    {
        // close calendar if we click off of it
        if (eventData.pointerCurrentRaycast.gameObject == BG)
        {
            gameObject.SetActive(false);
        }
    }
}
