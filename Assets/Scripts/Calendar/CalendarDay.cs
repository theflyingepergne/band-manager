using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;

public class CalendarDay : MonoBehaviour
{
    //---References---//
    [Header("UI References")]
    [SerializeField] private TMP_Text dayNo;
    [SerializeField] private TMP_Text eventText;
    [SerializeField] private GameObject currentDayMarker;

    [Header("Current Day Marker Tween controls")]
    [SerializeField] private float strength = 1.4f;
    [SerializeField] private float duration = 0.4f;
    [SerializeField] private int vibrato = 1;
    [SerializeField] private int elasticity = 1;

    public List<ScheduledEvent> scheduledEvents = new();
    public string eventTextBlock { get; private set; }
    public GameDate date { get; private set; }

    //---Local References---//
    Tween currentDayMarkerTween;
    Button button;

    //---Events---//
    public static System.Action<List<ScheduledEvent>> OnInspectDay;

    private void OnEnable()
    {
        // Setup button
        button = gameObject.GetComponent<Button>();
        button.onClick.AddListener(() => OnDayClicked());
    }

    public void SetupDay(GameDate setupDate, List<ScheduledEvent> events)
    {
        date = setupDate;
        scheduledEvents = events;

        // Use day (from calendarManager) as day number
        dayNo.text = date.day.ToString();


        // If list of events is not null, add each event title to eventTextBlock
        if (events != null)
        {
            foreach (ScheduledEvent e in events)
            {
                TMP_Text newLine = Instantiate(eventText, transform, false);
                SetFontStyle(e, newLine);

                newLine.text = "- " + e.gameEventData.title;
                // create a text block of event names
                // eventTextBlock += "- " + e.gameEventData.title + "\n";
            }
        }

        // Display finished eventTextBlock 
        eventText.text = eventTextBlock;

        // If CalendarDay == date.day, show currentDayMarker
        if (date.isSameDate(DateManager.Instance.date))
        {
            currentDayMarker.SetActive(true);
            currentDayMarkerTween = currentDayMarker.transform.DOPunchScale
            (
                Vector2.one * strength,
                duration,
                vibrato,
                elasticity
            );
        }
        else
        {
            currentDayMarker.SetActive(false);
        }
    }

    private void SetFontStyle(ScheduledEvent scheduledEvent, TMP_Text text)
    {
        // If date is in the past OR complete, strikethrough text
        if (scheduledEvent.date.isBeforeDate(date) || scheduledEvent.isCompleted)
        {
            text.fontStyle = FontStyles.Strikethrough | FontStyles.Bold;
        }
        else
        {
            text.fontStyle = FontStyles.Bold;
        }
    }

    private void OnDisable()
    {
        if (currentDayMarkerTween.IsActive())
        {
            currentDayMarkerTween.Kill();
            currentDayMarkerTween = null;
        }
    }

    public void OnDayClicked()
    {
        // Only invoke event if CalendarDay has text
        if (scheduledEvents.Count > 0)
        {
            OnInspectDay?.Invoke(scheduledEvents);
        }
        Debug.Log($"Pressed button day: {dayNo.text}");
    }
}
