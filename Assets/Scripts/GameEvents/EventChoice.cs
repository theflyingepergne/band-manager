using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class EventChoice
{
    public string choiceLabel;
    public List<StatChange> StatChanges; // If no stat changes, leave empty
    public List<CustomGameEventData> CustomEvents; // drag in custom events
}