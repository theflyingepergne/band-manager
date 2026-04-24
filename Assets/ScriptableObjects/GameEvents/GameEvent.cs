using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "GameEvent", menuName = "Game Events/Game Event")]
public class GameEvent : ScriptableObject
{
    public string eventTitle;
    [TextArea] public string description;
    public Sprite eventIllustration;

    public List<EventChoice> choices;
}