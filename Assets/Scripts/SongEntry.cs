using UnityEngine;

[System.Serializable]
public class SongEntry
{
    [Header("Info")]
    public string songName = "It's a song";
    public BandMemberData author;
    // public List<GenreData> songGenres;
    // public List<InstrumentData> songInstruments;

    [Header("Stats")]
    public float songScore = 10f;
    // Eventually songScore will be a calculated value
    // Add things here that will be used to calculate songScore
    
    public SongEntry(string name, BandMemberData bandMember, float score)
    {
        songName = name;
        author = bandMember;
        songScore = score;
    }
}
