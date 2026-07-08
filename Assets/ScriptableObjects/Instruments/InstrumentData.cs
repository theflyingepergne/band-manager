using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InstrumentData", menuName = "Instruments/InstrumentData")]
public class InstrumentData : ScriptableObject
{
    //---Core Info---//
    public string instrumentName;
    public Sprite sprite;
    public string description;

    //---Qualities---//
    [Range(1, 5)]
    public int difficultyLevel;
    // 1 being easy, 5 being hard
    // maybe works in tandem with talent level

    public GenreWeights genreAffinities;
    // somehow combines with bandMember genreAffinities to calculate song score
    
    public List<string> instrumentTraits;
    // e.g., "Good for Solos", "Good for small venues"
    // just strings for now, will eventually be used to calculate score
    // or trigger GameEvents e.g piano needs retuning, will cost £10,000.00
}
