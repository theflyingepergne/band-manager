using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameEventDatabase", menuName = "Game Events/GameEventDatabase")]
public class GameEventDatabase : ScriptableObject
{
    public List<GameEventData> events;

    public GameEventData GetRandomEvent()
    {
        if (events != null && events.Count > 0)
        {
            return events[Random.Range(0, events.Count)];
        }
        else
        {
            return null;
        }
    }
}