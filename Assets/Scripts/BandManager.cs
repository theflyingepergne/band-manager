using System.Collections.Generic;
using UnityEngine;

public class BandManager : Singleton<BandManager>
{
    //---References---//
    public List<BandMemberData> bandMembers = new List<BandMemberData>();
    public List<SongEntry> songCollection = new List<SongEntry>();
    public List<SongEntry> activeSetlist = new List<SongEntry>();
    public VenueData destinationVenue;

    //---Stats---//
    public float money;
    public int fans;
    public float chemistry;

    //---Stat Methods---//
    public void ApplyStatChange(StatChange effect)
    {
        switch (effect.stat)
        {
            case StatChange.StatType.Money:
                money += effect.amount;
                break;
            case StatChange.StatType.Fans:
                fans += (int)effect.amount;
                break;
            case StatChange.StatType.Chemistry:
                chemistry = Mathf.Clamp(chemistry + effect.amount, 0f, 100f);
                break;
        }
        Debug.Log($"{effect.stat} changed by {effect.amount}!");
    }

    //---Band Member Methods---//
    public void RecruitMember(BandMemberData data)
    {
        if (!bandMembers.Contains(data))
        {
            bandMembers.Add(data);
            // Debug.Log($"Hired {data.name}! {bandMembers.Count} band members");
        }
        else
        {
            Debug.Log("Error, no data found during hire");
        }
    }

    //---Song Methods---//
    public void AddSongToCollection(SongEntry song)
    {
        songCollection.Add(song);
        // Debug.Log($"Added {song.songName}! {songCollection.Count} songs in collection");
        Debug.Log($"Added {song.songName}, score = {song.songScore}");
    }

    //---Game Event Methods---//
    public void GenerateRandomSongFromEvent()
    {
        string songName = "Default song name";
        BandMemberData bandMember = null;
        float newSongScore = Random.Range(60f, 100f);

        if (bandMembers != null || bandMembers.Count != 0)
        {
            // pick a random band member to supply song data
            int i = Random.Range(0, bandMembers.Count);
            bandMember = bandMembers[i];
            songName = $"{bandMember.name}'s song";
        }

        SongEntry newSong = new SongEntry(songName, bandMember, newSongScore);
        BandManager.Instance.AddSongToCollection(newSong);
    }
    
    //---Setlist Methods---//
    public List<SongEntry> PrepareSetlist()
    {
        if (songCollection.Count > 0)
        {
            // init temporary lists of songEntries
            List<SongEntry> selectedSongs = new List<SongEntry>();
            List<SongEntry> shuffled = new List<SongEntry>(songCollection);

            // shuffle the elements in the 'shuffled' list
            for (int i = 0; i < shuffled.Count; i++)
            {
                int randIndex = Random.Range(i, shuffled.Count);

                SongEntry temp = shuffled[i];
                shuffled[i] = shuffled[randIndex];
                shuffled[randIndex] = temp;
            }

            // pick max 6 SongEntry from the 'shuffled' list
            for (int i = 0; i < 6 && i < shuffled.Count; i++)
            {
                selectedSongs.Add(shuffled[i]);
            }
            activeSetlist = selectedSongs;
        }
        
        return activeSetlist;
    }

    //---Venue Methods---//
    public void SetVenue(VenueData chosenVenue)
    {
        if (chosenVenue != null)
        {
            destinationVenue = chosenVenue;
            // Debug.Log("venue data set!");
        }
    }
}