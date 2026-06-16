[System.Serializable]
public class ScheduledGig
{
    public string venueID;
    public GameDate date;
    public bool isCompleted;

    [System.NonSerialized]
    public VenueData venueData;
}