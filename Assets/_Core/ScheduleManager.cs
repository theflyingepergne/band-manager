using System.Collections.Generic;
using UnityEngine;

public class ScheduleManager
{
    public static ScheduleManager Instance { get; private set; }

    //---References---//
    public List<GameEventData> allGameEvents;
    public List<ScheduledEvent> scheduledEvents;

    //---Local References---//
    DateManager dm;
    GameDate date;

    //---Methods---//
    public static void Initialize()
    {
        Instance = new ScheduleManager();
        Debug.Log("Initialized ScheduleManager");
        // TODO: load scheduledEvents from SaveLoadSystem

        // Initialize lists
        Instance.allGameEvents = new List<GameEventData>();
        Instance.scheduledEvents = new List<ScheduledEvent>();

        // Pull date from dateManager
        Instance.dm = DateManager.Instance;
        Instance.date = Instance.dm.date;

        // Listen for date changes
        DateManager.OnDateChanged += Instance.HandleDateChanged;

        // Load GameEventDatabase from Resources/GameEvents folder
        GameEventDatabase database = Resources.Load<GameEventDatabase>("GameEvents/GameEventDatabase");

        if (database != null && database.events != null)
        {
            // Copy GameEventData list from GameEventDatabase
            Instance.allGameEvents.AddRange(database.events);
            Debug.Log($"ScheduleManager initialized and loaded {Instance.allGameEvents.Count} events from the database asset.");
        }
        else
        {
            Debug.LogError("ScheduleManager Error: Could not find 'GameEventDatabase' asset in a Resources folder!");
        }

        Instance.ScheduleEvents();
    }

    private void ScheduleEvents()
    {
        foreach (GameEventData gameEventData in allGameEvents)
        {
            // For each GameEvent, create a ScheduledEvent with either the fixed date or generate random date
            ScheduledEvent scheduledEvent = new() { gameEventData = gameEventData };

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

    public List<GameEventData> GetTodaysEvents()
    {
        List<GameEventData> todaysEvents = new();

        foreach (ScheduledEvent s in scheduledEvents)
        {
            if (s.date.isSameDate(date))
            {
                todaysEvents.Add(s.gameEventData);
            }
        }

        Debug.Log($"Found {todaysEvents.Count} events today");
        return todaysEvents;
    }

    private GameDate GenerateRandomDate()
    {
        int randomMonth = Random.Range(dm.date.month, dm.date.month + 2);
        int randomDay = Random.Range(1, MonthList.Months[randomMonth].Days + 1);

        GameDate randomDate = new(randomDay, randomMonth, dm.date.year);
        // Debug.Log($"Created random date: {randomDate.GetDateAsString()}");
        return randomDate;
    }

    private void HandleDateChanged(GameDate newDate)
    {
        date = newDate;
    }
}