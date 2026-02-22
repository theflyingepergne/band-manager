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
    [SerializeField] private List<InstrumentData> instruments;
    [CreateProperty]
    public string instrumentsDisplay => (instruments.Count > 0)                 // Check if there are more than 0 instruments in the list 
        ? string.Join(", ", instruments.ConvertAll(i => i.instrumentName))     // This will allow us to display the instruments in the UI as a comma-separated list
        : "Willing to learn";                                                   // If there are no instruments, display "No instruments" in

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
    [SerializeField] private List<GenreData> genres;
    [CreateProperty]
    public string genresDisplay => (genres.Count > 0)
        ? string.Join(", ", genres.ConvertAll(g => g.genreName))
        : "Versatile";
    
    //-- Traits
    [SerializeField] private List<string> traits;
    [CreateProperty]
    public string traitsDisplay => string.Join(", ", traits);
}
