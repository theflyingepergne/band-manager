using System;
using System.Collections.Generic;
using Unity.Properties;
using UnityEngine;

[CreateAssetMenu(fileName = "BandMemberData", menuName = "Scriptable Objects/BandMemberData")]
public class BandMemberData : ScriptableObject
{
    public string memberName;

    [SerializeField] private List<string> instruments;
    [CreateProperty]
    public string instrumentsDisplay => string.Join(", ", instruments); // This will allow us to display the instruments in the UI as a comma-separated list

    [Range(0, 10)]  // This will allow us to set the talent level in the inspector with a slider from 0 to 10
    [SerializeField] private int _talentLevel;  // allow us to set the talent level in the inspector
    [CreateProperty]    // Point the Property to that variable
    public int talentLevel 
    { 
        get => _talentLevel; 
        set => _talentLevel = value; 
    }

    public Sprite memberSprite;

    [SerializeField] private List<string> genres;
    [CreateProperty]
    public string genresDisplay => string.Join(", ", genres); // This will allow us to display the genres in the UI as a comma-separated list
    
    [SerializeField] private List<string> traits;
    [CreateProperty]
    public string traitsDisplay => string.Join(", ", traits); // This will allow us to display the traits in the UI as a comma-separated list
}
