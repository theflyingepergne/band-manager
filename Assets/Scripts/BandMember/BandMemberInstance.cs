using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class BandMemberInstance
{
    //---Core vars---//
    public string id;
    public string name;
    public int talentLevel;
    public Sprite sprite;
    public GenreWeights genreAffinities;
    public List<InstrumentInstance> instruments;
    public List<TraitInstance> traits;

    //---Recruitment vars---//
    public TextAsset inkRecruitmentDialogue;
    public bool isRecruited = false;
    public bool wasRecruited = false;
    public float recruitThreshold;

    //---Home vars---//
    public TextAsset inkHomeDialogue;

    //---Constructor---//
    public BandMemberInstance(BandMemberData data = null)
    {
        id = data.name;

        name = data.memberName;
        talentLevel = data.talentLevel;
        sprite = data.memberSprite;

        genreAffinities = data.genreAffinities;
        recruitThreshold = data.recruitThreshold;

        // convert instruments
        instruments = data.instruments
            .Select(i => new InstrumentInstance(i))
            .ToList();

        // convert traits
        traits = data.traits
            .Select(t => new TraitInstance(t, this))
            .ToList();

        inkRecruitmentDialogue = data.inkRecruitmentDialogue;
        inkHomeDialogue = data.inkHomeDialogue;
}

//---Methods---//
public void AdjustRecruitmentThreshold(float amount)
    {
        recruitThreshold -= amount;
    }
}