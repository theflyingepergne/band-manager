using System;
using System.Collections.Generic;
using Unity.Properties;
using UnityEngine;

[CreateAssetMenu(fileName = "BandMemberData", menuName = "Scriptable Objects/BandMemberData")]
public class BandMemberData : ScriptableObject
{
    [Header("Core Details")]
    public string memberName;
    public Sprite memberSprite;

    [Header("Song Writing")]
    public List<SongEntry> songsWritten;
    [Range(0, 10)]                              // Create inspector slider from 0 to 10
    [SerializeField] private int _talentLevel;  // allow us to set the talent level in the inspector
    [CreateProperty]                            // Point the Property to that variable
    public int talentLevel
    { 
        get => _talentLevel; 
        set => _talentLevel = value; 
    }
    public List<InstrumentData> instruments;
    public GenreWeights genreAffinities;
    // affinities vs weights
    // same thing, different names:
    // affinities = the effect ON something
    // weights = the weights they HAVE (calculated by affinities)

    [Header("Personality")]
    public List<TraitData> traits;

    [Header("Recruitment")]
    [Range(0, 1)]
    public float recruitThreshold;
    public TextAsset inkRecruitmentDialogue;
}
