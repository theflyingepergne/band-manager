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
        Instance.scheduledEvents = new List<ScheduledEvent>();
        Instance.allVenues = new List<VenueData>();
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

    //---Loading Databases---//
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
        // Load GameEventDatabase from Resources/Databases folder
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

    //---Scheduling Game Events---//
    private void ScheduleEvents()
    {
        foreach (GameEventData gameEventData in allGameEvents)
        {
            // Create a saveable scheduledEvent instance
            ScheduledEvent scheduledEvent = new()
            {
                gameEventData = gameEventData,
                eventID = gameEventData.name,
                title = gameEventData.title,
                description = gameEventData.description,
            };

            // Assign any random dates
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

    public void ScheduleNewEvent
    (
        GameEventData gameEventData,
        GameDate date,
        string newTitle = "",
        string newDescription = ""
    )
    {
        scheduledEvents.Add(new ScheduledEvent
        {
            gameEventData = gameEventData,
            title = string.IsNullOrEmpty(newTitle) ? gameEventData.title : newTitle,
            description = string.IsNullOrEmpty(newDescription) ? gameEventData.description : newDescription,
            eventID = gameEventData.name,
            date = date
        });
    }

    public List<ScheduledEvent> GetTodaysEvents()
    {
        List<ScheduledEvent> todaysEvents = new();

        foreach (ScheduledEvent s in scheduledEvents)
        {
            if (s.date.IsSameDate(date) && s.isCompleted == false)
            {
                todaysEvents.Add(s);
            }
        }

        // Debug.Log($"Found {todaysEvents.Count} events today");
        return todaysEvents;
    }

    //---Scheduling Gigs---//
    public void ScheduleNewGig(VenueData venue, GameDate date)
    {
        scheduledGigs.Add(new ScheduledGig
        {
            venueData = venue,
            venueID = venue.name,
            date = date
        });
    }

    public (bool anyGigs, ScheduledGig scheduledGig) CheckAnyGigsOnDay(GameDate queryDate)
    {
        foreach (ScheduledGig gig in scheduledGigs)
        {
            if (bm.destinationVenue == gig.venueData
                && gig.date.IsSameDate(queryDate)
                && gig.isCompleted == false)
            {
                return (true, gig);
            }
        }

        // If we complete the loop, there must be no gigs today
        return (false, null);
    }

    //---Dates---//
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