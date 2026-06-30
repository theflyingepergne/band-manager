// Scriptable Object to hold data for each band member
using System;
using System.Collections.Generic;
using Unity.Properties;
using UnityEngine;

[CreateAssetMenu(fileName = "BandMemberData", menuName = "Scriptable Objects/BandMemberData")]
public class BandMemberData : ScriptableObject
{
    //---Name---//
    public string memberName;

    //---Instruments---//
    public List<InstrumentData> instruments;

    //---Talent Level---//
    [Range(0, 10)]                              // Create inspector slider from 0 to 10
    [SerializeField] private int _talentLevel;  // allow us to set the talent level in the inspector
    [CreateProperty]                            // Point the Property to that variable
    public int talentLevel
    { 
        get => _talentLevel; 
        set => _talentLevel = value; 
    }

    public Sprite memberSprite;

    //---Genres
    public List<GenreData> genres;

    //---Traits---//
    public List<string> traits;

    //---Songs---//
    public List<SongEntry> songsWritten = new();
}
