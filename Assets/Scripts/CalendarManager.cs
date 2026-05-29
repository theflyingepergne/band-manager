using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CalendarManager : MonoBehaviour
{
    //---References---//
    [Header("UI References")]
    [SerializeField] private RectTransform gridContainer;
    [SerializeField] private TMP_Text monthText;
    [SerializeField] private TMP_Text yearText;


    [Header("Prefab references")]
    [SerializeField] private GameObject calendarDayPrefab;

    [Header("Test Game Event")]
    [SerializeField] private List<GameEventData> gameEventData;

    //---Methods---//
    private void Start()
    {
        SetupCalendar();
    }

    private void SetupCalendar()
    {
        // create calendar day prefabs for each day in the month
        // currently each month is 28 days but maybe change this to real world amounts...
        for (int day = 0; day < 28; day++)
        {
            GameObject newCalendarDay = Instantiate(calendarDayPrefab, gridContainer, false);

            // randomly populate the calendar with events for now
            bool doDay = Random.value < 0.2f;
            if (doDay == true)
            {
                newCalendarDay.GetComponent<CalendarDay>().SetupDay(day + 1, gameEventData);
            }
            else
            {
                newCalendarDay.GetComponent<CalendarDay>().SetupDay(day + 1, null);
            }


        }

        // TODO: pull month/year text using some kind of date manager
        monthText.text = Month.January.ToString();
        yearText.text = "1979";
    }
}
