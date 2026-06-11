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
        if (fromTodaysDate == true)
        {
            // If choosing a date 'fromTodaysDate', just add how many days in the future - not months
            GameDate futureDate = DateManager.Instance.date.AddDays(date.day);
            ScheduleManager.Instance.ScheduleNewEvent(gameEventData, futureDate);
        }
        else
        {
            ScheduleManager.Instance.ScheduleNewEvent(gameEventData, date);
        }
    }
}