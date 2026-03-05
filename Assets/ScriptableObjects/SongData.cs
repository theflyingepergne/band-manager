using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SongData", menuName = "Scriptable Objects/SongData")]
public class SongData : ScriptableObject
{
    [Header("Info")]
    public string songName;
    public List<GenreData> songGenres;
    public List<InstrumentData> songInstruments;

    [Header("Stats")]
    public float songScore;
    // other stuff
}
