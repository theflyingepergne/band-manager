using UnityEngine;

[System.Serializable]
public class BookFutureGig : CustomGameEvent
{
    //---References---//
    [Header("Future Gig Details")]
    public GameEventData gameEventData;
    public GameDate date;
    public bool fromTodaysDate;
    public VenueData venueData;

    public override void Execute()
    {
        GameDate futureDate;

        if (fromTodaysDate == true)
        {
            // If choosing a date 'fromTodaysDate', just add how many days in the future - not months
            futureDate = DateManager.Instance.date.AddDays(date.day);
        }
        else
        {
            futureDate = date;
        }

        // Schedule both an event AND the gig
        // This will create an event pop-up and tell the venue there is a gig that day
        ScheduleManager.Instance.ScheduleNewEvent(gameEventData, futureDate, $"Gig at {venueData.name}");
        ScheduleManager.Instance.ScheduleNewGig(venueData, futureDate);
    }
}