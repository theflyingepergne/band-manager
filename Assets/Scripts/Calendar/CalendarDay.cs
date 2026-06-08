using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class CalendarDay : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private TMP_Text dayNo;
    [SerializeField] private TMP_Text eventText;
    [SerializeField] private GameObject currentDayMarker;

    public void SetupDay(int day, List<ScheduledEvent> events)
    {
        dayNo.text = day.ToString();

        string eventTextBlock = "";

        if (events != null)
        {
            foreach (ScheduledEvent e in events)
            {
                // create a text block of event names
                eventTextBlock += "- " + e.gameEventData.title + "\n";
            }
        }
        eventText.text = eventTextBlock;

        if (day == DateManager.Instance.date.day)
        {
            currentDayMarker.SetActive(true);
            currentDayMarker.transform.DOPunchScale(Vector2.one * 1.4f, 0.4f, 1);
        }
        else
        {
            currentDayMarker.SetActive(false);
        }
    }
}
