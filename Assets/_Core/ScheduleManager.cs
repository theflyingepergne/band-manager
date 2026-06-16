using System.Collections.Generic;
using UnityEngine;

public class ScheduleManager
{
    public static ScheduleManager Instance { get; private set; }

    //---References---//
    public List<GameEventData> allGameEvents;
    public List<VenueData> allVenues;
    public List<ScheduledEvent> scheduledEvents;
    public List<ScheduledGig> scheduledGigs;

    //---Local References---//
    DateManager dm;
    BandManager bm;
    GameDate date;

    //---Methods---//
    public static void Initialize()
    {
        Instance = new ScheduleManager();
        Debug.Log("Initialized ScheduleManager");
        // TODO: load scheduledEvents from SaveLoadSystem

        // Initialize lists
        Instance.allGameEvents = new List<GameEventData>();
        Instance.allVenues = new List<VenueData>();
        Instance.scheduledEvents = new List<ScheduledEvent>();
        Instance.scheduledGigs = new List<ScheduledGig>();

        // Pull date from dateManager
        Instance.dm = DateManager.Instance;
        Instance.bm = BandManager.Instance;
        Instance.date = Instance.dm.date;

        // Listen for date changes
        DateManager.OnDateChanged += Instance.HandleDateChanged;

        Instance.LoadGameEventDatabase();
        Instance.LoadVenueDatabase();

        Instance.ScheduleEvents();
    }

    private void LoadGameEventDatabase()
    {
        // Load GameEventDatabase from Resources/GameEvents folder
        GameEventDatabase database = Resources.Load<GameEventDatabase>("Databases/GameEventDatabase");

        if (database != null && database.events != null)
        {
            // Copy GameEventData list from GameEventDatabase
            Instance.allGameEvents.AddRange(database.events);
            Debug.Log($"ScheduleManager initialized and loaded {Instance.allGameEvents.Count} events from the database.");
        }
        else
        {
            Debug.LogError("ScheduleManager Error: Could not find 'GameEventDatabase' asset in a Resources folder!");
        }
    }

    private void LoadVenueDatabase()
    {
        // Load GameEventDatabase from Resources/GameEvents folder
        VenueDatabase database = Resources.Load<VenueDatabase>("Databases/VenueDatabase");

        if (database != null && database.venues != null)
        {
            // Copy GameEventData list from GameEventDatabase
            Instance.allVenues.AddRange(database.venues);
            Debug.Log($"ScheduleManager initialized and loaded {Instance.allVenues.Count} venues from the database.");
        }
        else
        {
            Debug.LogError("ScheduleManager Error: Could not find 'VenueDatabase' asset in a Resources folder!");
        }
    }

    private void ScheduleEvents()
    {
        foreach (GameEventData gameEventData in allGameEvents)
        {
            // Create a fresh tracking instance
            ScheduledEvent scheduledEvent = new()
            {
                gameEventData = gameEventData,
                eventID = gameEventData.name
            };

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

        // Debug.Log($"Found {todaysEvents.Count} events today");
        return todaysEvents;
    }

    public void ScheduleNewGig(VenueData venue, GameDate date)
    {
        scheduledGigs.Add(new ScheduledGig
        {
            venueData = venue,
            venueID = venue.name,
            date = date
        });
    }

    public bool CheckGigToday()
    {
        foreach (ScheduledGig gig in scheduledGigs)
        {
            if (bm.destinationVenue == gig.venueData && gig.date.isSameDate(date))
            {
                return true;
            }
        }

        // If we complete the loop, there must be no gigs today
        return false;
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