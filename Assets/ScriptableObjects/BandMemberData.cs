// Scriptable Object to hold data for each band member
using System;
using System.Collections.Generic;
using Unity.Properties;
using UnityEngine;

[CreateAssetMenu(fileName = "BandMemberData", menuName = "Scriptable Objects/BandMemberData")]
public class BandMemberData : ScriptableObject
{
    //-- Setting out Band Member Data
    //-- Name
    public string memberName;

    //-- Instruments
    [SerializeField] public List<InstrumentData> instruments;

    //-- Talent Level
    [Range(0, 10)]                              // This will allow us to set the talent level in the inspector with a slider from 0 to 10
    [SerializeField] private int _talentLevel;  // allow us to set the talent level in the inspector
    [CreateProperty]                            // Point the Property to that variable
    public int talentLevel 
    { 
        get => _talentLevel; 
        set => _talentLevel = value; 
    }

    //-- Sprite
    public Sprite memberSprite;

    //-- Genres
    [SerializeField] public List<GenreData> genres;
    
    //-- Traits
    [SerializeField] public List<string> traits;

    //-- Songs
    public List<SongData> songsWritten = new List<SongData>();
}
