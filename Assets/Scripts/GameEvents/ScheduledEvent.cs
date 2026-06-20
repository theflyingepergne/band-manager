[System.Serializable]
public class ScheduledEvent
{
    public string eventID;
    public string title;
    public string description;
    public GameDate date;
    public bool isCompleted;

    [System.NonSerialized]
    public GameEventData gameEventData;
}
