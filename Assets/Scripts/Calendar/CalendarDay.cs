using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CalendarDay : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private TMP_Text dayNo;
    [SerializeField] private TMP_Text eventText;
    [SerializeField] private GameObject currentDayMarker;

    public void SetupDay(int day, List<GameEventData> events)
    {
        dayNo.text = day.ToString();

        string eventTextBlock = "";

        if (events != null)
        {
            foreach (GameEventData e in events)
            {
                // create a text block of event names
                eventTextBlock += "- " + e.title + "\n";
            }
        }
        eventText.text = eventTextBlock;

        if (day == DateManager.Instance.day)
        {
            currentDayMarker.SetActive(true);
        }
        else
        {
            currentDayMarker.SetActive(false);
        }
    }

}
