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
    [SerializeField] private RectTransform gridSprite;
    [SerializeField] private TMP_Text monthText;
    [SerializeField] private TMP_Text yearText;
    [SerializeField] private GameObject BG;

    [Header("Sprite references")]
    [SerializeField] private Sprite calendar4Rows;
    [SerializeField] private Sprite calendar5Rows;


    [Header("Prefab references")]
    [SerializeField] private GameObject calendarDayPrefab;

    [Header("Test Game Event")]
    [SerializeField] private List<GameEventData> gameEventData;

    //---Local References---//
    DateManager dm;
    public int day = 1;
    public int month = 1;
    public int year = 1979;

    //---Events---//

    //---Methods---//
    private void OnEnable()
    {
        dm = DateManager.Instance;
        DateManager.OnDateChanged += HandleDateChanged;
        SetupCalendar();
    }

    private void OnDisable()
    {
        DateManager.OnDateChanged -= HandleDateChanged;
    }

    private void SetupCalendar()
    {
        day = dm.day;
        month = dm.month;
        year = dm.year;

        ClearCalendar();

        // If month is feb, use the small calendar
        if (month == 2)
        {
            gridSprite.GetComponent<Image>().sprite = calendar4Rows;
            calendarBorder.SetSizeWithCurrentAnchors(
                RectTransform.Axis.Vertical,
                795f
            );
        }
        else
        {
            gridSprite.GetComponent<Image>().sprite = calendar5Rows;
            calendarBorder.SetSizeWithCurrentAnchors(
                RectTransform.Axis.Vertical,
                965f
            );
        }

        for (int d = 1; d < MonthList.Months[month].Days + 1; d++)
        {
            GameObject newCalendarDay = Instantiate(calendarDayPrefab, gridContainer, false);

            // // randomly populate the calendar with events for now
            // bool doDay = Random.value < 0.2f;
            // if (doDay == true)
            // {
            //     newCalendarDay.GetComponent<CalendarDay>().SetupDay(day + 1, gameEventData);
            // }
            // else
            // {
            //     newCalendarDay.GetComponent<CalendarDay>().SetupDay(day + 1, null);
            // }

            List<GameEventData> eventsToAdd = new List<GameEventData>();

            foreach (GameEventData g in gameEventData)
            {
                if (g.date.day == d && g.date.month == month && g.date.year == year)
                {
                    eventsToAdd.Add(g);
                }
            }

            newCalendarDay.GetComponent<CalendarDay>().SetupDay(d, eventsToAdd);
        }

        monthText.text = MonthList.Months[month].Name;
        yearText.text = year.ToString();
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

    private void HandleDateChanged(int newDay, int newMonth, int newYear)
    {
        day = newDay;
        month = newMonth;
        year = newYear;
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
