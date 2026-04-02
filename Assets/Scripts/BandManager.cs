using System.Collections.Generic;
using UnityEngine;

public class BandManager : Singleton<BandManager>
{
    public List<BandMemberData> bandMembers = new List<BandMemberData>();
    public List<SongEntry> songCollection = new List<SongEntry>();

    public void RecruitMember(BandMemberData data)
    {
        if (!bandMembers.Contains(data))
        {
            bandMembers.Add(data);
            Debug.Log($"Hired {data.name}! {bandMembers.Count} band members");
        }
        else
        {
            Debug.Log("Error, no data found during hire");
        }
    }

    public void AddSongToCollection(SongEntry song)
    {
        songCollection.Add(song);
        Debug.Log($"Added {song.songName}! {songCollection.Count} songs in collection");
    }
}