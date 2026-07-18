[System.Serializable]
public class ScheduledEvent
{
    //---Core Info---//
    public string eventID;
    public string title;
    public string description;
    public GameDate date;

    //---Completion---//
    public bool isCompleted;

    //---Visibility---//
    public bool onlyShowOnCompleted;

    [System.NonSerialized]
    public GameEventData gameEventData;

    //---Constructor---//
    public ScheduledEvent
    (
        GameEventData gameEventData,
        GameDate date = new(),
        string title = "",
        string description = "",
        bool onlyShowOnCompleted = false
    )
    {
        this.gameEventData = gameEventData;
        this.title = string.IsNullOrEmpty(title) ? gameEventData.title : title;
        this.description = string.IsNullOrEmpty(description) ? gameEventData.description : description;
        eventID = gameEventData.name;
        this.date = date;
        this.onlyShowOnCompleted = gameEventData.onlyShowOnCompleted;
    }
}