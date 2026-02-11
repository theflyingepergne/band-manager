// Scriptable object to hold data about instruments
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InstrumentData", menuName = "Scriptable Objects/InstrumentData")]
public class InstrumentData : ScriptableObject
{
    [SerializeField] public string instrumentName;
    [SerializeField] public Sprite instrumentSprite;
    [SerializeField] public string description;
    [Range(1, 5)]                                          // 1 being easy, 5 being hard. maybe works in tandem with talent level
    [SerializeField] public int difficultyLevel;
    [SerializeField] public List<string> genres;                 // e.g., "Rock, Jazz, Classical"
    [SerializeField] public List<string> instrumentTraits; //  e.g., "Good for Solos", "Good for small venues"
}
