using UnityEngine;

[System.Serializable]
public class RecruitableBandMember
{
    public string id;
    public string name;
    public int talentLevel;
    public Sprite sprite;

    public float recruitThreshold;

    public void AdjustRecruitmentThreshold(float amount)
    {
        recruitThreshold -= amount;
    }
}
