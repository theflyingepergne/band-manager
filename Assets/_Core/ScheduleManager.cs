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

        // Load GameEventDatabase from Resources/GameEvents folder
        GameEventDatabase database = Resources.Load<GameEventDatabase>("GameEvents/GameEventDatabase");

        if (database != null && database.events != null)
        {
            // Copy GameEventData list from GameEventDatabase
            Instance.gameEvents.AddRange(database.events);
            Debug.Log($"ScheduleManager initialized and loaded {Instance.gameEvents.Count} events from the database asset.");
        }
        else
        {
            Debug.LogError("ScheduleManager Error: Could not find 'GameEventDatabase' asset in a Resources folder!");
        }

        Instance.ScheduleEvents();
    }

    private void ScheduleEvents()
    {
        foreach (GameEventData gameEventData in gameEvents)
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

    private GameDate GenerateRandomDate()
    {
        int randomMonth = Random.Range(dm.date.month, dm.date.month + 2);
        int randomDay = Random.Range(1, MonthList.Months[randomMonth].Days + 1);

        GameDate randomDate = new(randomDay, randomMonth, dm.date.year);
        Debug.Log($"Created random date: {randomDate.GetDateAsString()}");
        return randomDate;
    }
}