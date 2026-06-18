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

        ClearEventText();

        // If list of events is not null, add new line for each event
        if (events != null)
        {
            foreach (ScheduledEvent e in events)
            {
                TMP_Text newLine = Instantiate(eventText, transform, false);
                newLine.gameObject.SetActive(true);
                SetFontStyle(e, newLine);

                // set event title as text
                newLine.text = "- " + e.gameEventData.title;
            }
        }

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

    private void ClearEventText()
    {
        // Deactivate the default, testing text so it don't take up space
        eventText.gameObject.SetActive(false);
    }
}
