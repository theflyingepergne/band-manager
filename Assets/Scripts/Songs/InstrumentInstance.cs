using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InstrumentInstance
{
    //---Core Info---//
    public string id;
    public string instrumentName;
    public Sprite sprite;
    public string description;

    //---Qualities---//
    public int difficultyLevel;
    public GenreWeights genreAffinities;
    public List<string> instrumentTraits;

    //---Per-Instrument Info---//
    // owner
    // condition
    // value

    //---Constructor---//
    public InstrumentInstance(InstrumentData data)
    {
        id = data.name;
        instrumentName = data.instrumentName;
        sprite = data.sprite;
        description = data.description;
        difficultyLevel = data.difficultyLevel;
        genreAffinities = data.genreAffinities;
        instrumentTraits = data.instrumentTraits;
    }
}