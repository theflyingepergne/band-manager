using System.Collections.Generic;
using UnityEngine;

public class BandManager : Singleton<BandManager>
{
    public List<BandMemberData> HiredMembers = new List<BandMemberData>();

    public void RecruitMember(BandMemberData data)
    {
        if (!HiredMembers.Contains(data))
        {
            HiredMembers.Add(data);
            Debug.Log($"Hired {data.name}! Total members: {HiredMembers.Count}");
        }
        else
        {
            Debug.Log("Error, no data found during hire");
        }
    }
}