using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class EventChoice
{
    public string choiceLabel;
    [TextArea] public string choiceOutcomeDescription;
    public List<StatChange> StatChanges; // If no stat changes, leave empty

    [SerializeReference, SubclassSelector]
    public List<CustomGameEvent> CustomEvents; // drag in custom events
}