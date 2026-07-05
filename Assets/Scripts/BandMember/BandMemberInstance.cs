using UnityEngine;

[System.Serializable]
public class BandMemberInstance
{
    //---Core vars---//
    public string id;
    public string name;
    public int talentLevel;
    public Sprite sprite;
    // genres
    // instruments

    //---Recruitment vars---//
    public bool isRecruited = false;
    public bool wasRecruited = false;
    public float recruitThreshold;

    //---Methods---//
    public void AdjustRecruitmentThreshold(float amount)
    {
        recruitThreshold -= amount;
    }
}
