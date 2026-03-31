using System.Collections.Generic;
using UnityEngine;

public class BandManager : Singleton<BandManager>
{
    public List<BandMemberData> BandMembers = new List<BandMemberData>();

    public void RecruitMember(BandMemberData data)
    {
        if (!BandMembers.Contains(data))
        {
            BandMembers.Add(data);
            Debug.Log($"Hired {data.name}! Total members: {BandMembers.Count}");
        }
        else
        {
            Debug.Log("Error, no data found during hire");
        }
    }
}