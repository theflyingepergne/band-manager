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
            // Create a fresh tracking instance
            ScheduledEvent scheduledEvent = new();

            // Connect the live asset reference for gameplay use right now
            scheduledEvent.gameEventData = gameEventData;

            // Record its file name so the Save/Load system can find it later!
            scheduledEvent.eventID = gameEventData.name;

            // Assign the date to the TRACKING instance, leaving the asset untouched
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

    public void ScheduleNewEvent(GameEventData gameEventData, GameDate date)
    {
        // Trying out a different way of initializing object
        scheduledEvents.Add(new ScheduledEvent
        {
            gameEventData = gameEventData,
            eventID = gameEventData.name,
            date = date
        });
    }

    public List<ScheduledEvent> GetTodaysEvents()
    {
        List<ScheduledEvent> todaysEvents = new();

        foreach (ScheduledEvent s in scheduledEvents)
        {
            if (s.date.isSameDate(date) && s.isCompleted == false)
            {
                todaysEvents.Add(s);
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