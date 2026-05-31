using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CalendarDay : MonoBehaviour
{
    [SerializeField] private TMP_Text dayNo;
    [SerializeField] private TMP_Text eventText;

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


    }

}
