[System.Serializable]
public class ScheduledEvent
{
    public string eventID;
    public string eventTitle;
    public GameDate date;
    public bool isCompleted;

    [System.NonSerialized]
    public GameEventData gameEventData;
}
