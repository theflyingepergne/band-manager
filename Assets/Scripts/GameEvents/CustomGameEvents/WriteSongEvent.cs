using UnityEngine;

[System.Serializable]
public class WriteSongEvent : CustomGameEvent
{
    public override void Execute()
    {
        // Find a random member and make them write a song
        BandManager.Instance.GenerateRandomSongFromEvent();
    }
}