using UnityEngine;

[System.Serializable]
public class SongEntry
{
    [Header("Info")]
    public string songName = "It's a song";
    // public List<GenreData> songGenres;
    // public List<InstrumentData> songInstruments;

    [Header("Stats")]
    public float songScore = 10f;
    // other stuff

    public SongEntry(string name, float score)
    {
        songName = name;
        songScore = score;
    }
}
