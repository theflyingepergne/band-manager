using System.Collections.Generic;
using UnityEngine;

public class ScheduleManager
{
    public static ScheduleManager Instance { get; private set; }

    //---References---//
    public List<GameEventData> gameEvents;
    public List<ScheduledEvent> scheduledEvents;
    DateManager dm;

    //---Methods---//
    public static void Initialize()
    {
        Instance = new ScheduleManager();
        Debug.Log("Initialized ScheduleManager");
        // TODO: load scheduledEvents from SaveLoadSystem

        // Initialize lists
        Instance.gameEvents = new List<GameEventData>();
        Instance.scheduledEvents = new List<ScheduledEvent>();
        Instance.dm = DateManager.Instance;

        GameEventDatabase database = Resources.Load<GameEventDatabase>("GameEvents/GameEventDatabase");

        if (database != null && database.events != null)
        {
            // Copy the events out of the database into this manager's list
            Instance.gameEvents.AddRange(database.events);
            Debug.Log($"ScheduleManager initialized and loaded {Instance.gameEvents.Count} events from the database asset.");
        }
        else
        {
            Debug.LogError("ScheduleManager Error: Could not find 'GameEventDatabase' asset in a Resources folder!");
        }

        // Schedule events
        Instance.ScheduleEvents();
    }

    private void ScheduleEvents()
    {
        foreach (GameEventData gameEventData in gameEvents)
        {
            ScheduledEvent scheduledEvent = new ScheduledEvent();

            scheduledEvent.gameEventData = gameEventData;

            if (gameEventData.isFixedDate)
            {
                scheduledEvent.date = gameEventData.date;
            }
            else
            {
                scheduledEvent.date = GenerateRandomDate();
            }

            scheduledEvents.Add(scheduledEvent);
        }
    }

    private GameDate GenerateRandomDate()
    {
        int randomMonth = Random.Range(dm.date.month, dm.date.month + 2);
        int randomDay = Random.Range(dm.date.day, MonthList.Months[randomMonth].Days + 1);

        GameDate randomDate = new GameDate(randomDay, randomMonth, dm.date.year);
        return randomDate;
    }
}
