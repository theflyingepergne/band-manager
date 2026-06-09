using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CalendarManager : MonoBehaviour, IPointerClickHandler
{
    //---References---//
    [Header("UI References")]
    [SerializeField] private RectTransform calendarBorder;
    [SerializeField] private RectTransform gridContainer;
    [SerializeField] private TMP_Text monthText;
    [SerializeField] private TMP_Text yearText;
    [SerializeField] private GameObject BG;



    [Header("Prefab references")]
    [SerializeField] private GameObject calendarDayPrefab;

    //---Local References---//
    DateManager dm;
    ScheduleManager sm;
    private List<ScheduledEvent> scheduledEvents;
    public GameDate date = new GameDate(1, 1, 1979);

    //---Methods---//
    private void OnEnable()
    {
        dm = DateManager.Instance;
        sm = ScheduleManager.Instance;

        DateManager.OnDateChanged += HandleDateChanged;
        
        SetupCalendar();
    }

    private void OnDisable()
    {
        DateManager.OnDateChanged -= HandleDateChanged;
    }

    private void SetupCalendar()
    {
        date = dm.date;
        scheduledEvents = sm.scheduledEvents;

        ClearCalendar();


        // For however many days there are in the current month...
        for (int d = 1; d < MonthList.Months[date.month].Days + 1; d++)
        {
            // Create CalendarDay prefabs and parent to Calendar gridContainer
            GameObject newCalendarDay = Instantiate(calendarDayPrefab, gridContainer, false);

            // Initialize empty list of scheduledEvents to populate with eventsToAdd
            List<ScheduledEvent> eventsToAdd = new List<ScheduledEvent>();

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
            newCalendarDay.GetComponent<CalendarDay>().SetupDay(d, eventsToAdd);
        }

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

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.pointerCurrentRaycast.gameObject == BG)
        {
            gameObject.SetActive(false);
        }
    }
}
