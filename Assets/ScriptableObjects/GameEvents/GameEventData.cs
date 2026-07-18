using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "GameEventData", menuName = "Game Events/Game Event")]
public class GameEventData : ScriptableObject
{
    [Header("Scheduling (leave blank for random date)")]
    public bool isFixedDate;
    public GameDate date;

    [Header("Event details")]
    public string title;
    [TextArea] public string description;
    public Sprite sprite;

    public List<EventChoice> choices;

    [Header("Visibility")]
    public bool onlyShowOnCompleted = false;
}