using System.Collections.Generic;
using System.Net;
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


    public GameDate date = new GameDate(1, 1, 1979);

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
        date = dm.date;

        ClearCalendar();

        // If month is feb, use the small calendar
        if (date.month == 2)
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

        // For however many days there are in the current month...
        for (int d = 1; d < MonthList.Months[date.month].Days + 1; d++)
        {
            // Create CalendarDay prefabs and parent to Calendar gridContainer
            GameObject newCalendarDay = Instantiate(calendarDayPrefab, gridContainer, false);

            // Initialize empty list of GameEventData to populate with eventsToAdd
            List<GameEventData> eventsToAdd = new List<GameEventData>();

            // For each GameEventData in list of GameEventData (GameEventDatabase),
            // If the date of the GameEvent matches the date of the current iteration
            // Add it to a list of eventsToAdd
            foreach (GameEventData g in gameEventData)
            {
                if (g.date.day == d && g.date.month == date.month && g.date.year == date.year)
                {
                    eventsToAdd.Add(g);
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
