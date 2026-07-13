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

    //---Genres---//
    public GenreWeights genreAffinities;
    // affinities vs weights
    // same thing, different names:
    // affinities = the effect ON something
    // weights = the weights they HAVE (calculated by affinities)

    //---Traits---//
    [Range(0, 1)]
    public float recruitThreshold;
    public List<TraitData> traits;

    //---Songs---//
    public List<SongEntry> songsWritten;
}
