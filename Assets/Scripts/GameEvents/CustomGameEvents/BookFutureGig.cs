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

    //---Local References---//
    DateManager dm;


    public override void Execute()
    {
        dm = DateManager.Instance;
        // if (fromTodaysDate == true)
        // {
        //     GameDate futureDate = new(date.day + dm.date.day, date.month + dm.date.month;
        // }
        ScheduleManager.Instance.ScheduleNewEvent(gameEventData, date);
    }
}