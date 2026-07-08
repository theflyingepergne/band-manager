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

    //---Recruitment vars---//
    public bool isRecruited = false;
    public bool wasRecruited = false;
    public float recruitThreshold;

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
    }

    //---Methods---//
    public void AdjustRecruitmentThreshold(float amount)
    {
        recruitThreshold -= amount;
    }
}