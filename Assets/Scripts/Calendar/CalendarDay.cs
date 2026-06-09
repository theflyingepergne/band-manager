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

    [Header("Tween controls")]
    [SerializeField] private float strength = 1.4f;
    [SerializeField] private float duration = 0.4f;
    [SerializeField] private int vibrato = 1;
    [SerializeField] private int elasticity = 1;

    Tween currentDayMarkerTween;

    private void Awake()
    {
        currentDayMarkerTween = currentDayMarker.transform.DOPunchScale
            (
                Vector2.one *
                strength,
                duration,
                vibrato,
                elasticity
            );
    }

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
            currentDayMarkerTween.Play();
        }
        else
        {
            currentDayMarker.SetActive(false);
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
}
