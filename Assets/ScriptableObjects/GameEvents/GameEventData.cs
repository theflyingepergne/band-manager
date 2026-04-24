using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "GameEventData", menuName = "Game Events/Game Event")]
public class GameEventData : ScriptableObject
{
    public string title;
    [TextArea] public string description;
    public Sprite sprite;

    public List<EventChoice> choices;
}