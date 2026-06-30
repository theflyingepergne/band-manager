using System.Collections.Generic;
using UnityEngine;


//==================================================================//
//  this class will be responsible for instantiating all possible   //
//  band members that can be recruited                              //
//                                                                  //
//  this class will keep track of progress with recruitable band    //
//  members                                                         //
//==================================================================//

/*
    each day, 3 recruitable band members will appear in the RecruitBandMembers scene

    clicking on them will begin dialogue with them

    talking to them via certain choices will satisfy certain conditions

    if the sum of these conditions is over a 'joinThreshold',
    they will ask you if they can join the band or vice versa

    accepting will add them to the list of BandMembers in BandManager

    declining will either add them back to the pool of recruitable band members
    or they will be like "fuck u i don't need u" and join another band

    there will be a chance to see this band playing at a venue (random chance)
    
    in this case, the aforementioned joinThreshold will be higher but the recruitment
    process can be started again

    each time the recruitment process is started with the same recruitable band member
    the joinThreshold will be higher

    number of fans can make it easier to reach a recruitable band member's joinThreshold
*/

public class RecruitmentManager
{
    public static RecruitmentManager Instance { get; private set; }
    //---References---//
    public Dictionary<string, RecruitableBandMember> allRecruitableBandMembers = new();

    //---Local References---//
    private readonly List<BandMemberData> allBandMembers = new();

    //---Initialization Methods---//
    public static void Initialize()
    {
        Instance = new();
        Debug.Log("Initialized RecruitmentManager");

        Instance.LoadRecruitableBandMemberDatabase();

        Instance.SetupRecruitableBandMembers();
    }

    private void LoadRecruitableBandMemberDatabase()
    {
        BandMemberDatabase database = Resources.Load<BandMemberDatabase>("Databases/BandMemberDatabase");

        if (database != null && database.recruitableBandMembers != null)
        {
            // Copy BandMemberData list from BandMemberDatabase
            Instance.allBandMembers.AddRange(database.recruitableBandMembers);
            Debug.Log($"RecruitmentManager initialized and loaded {Instance.allBandMembers.Count} band members from the database.");
        }
        else
        {
            Debug.LogError("RecruitmentManager Error: Could not find 'BandMemberDatabase' asset in a Resources folder!");
        }
    }

    private void SetupRecruitableBandMembers()
    {
        foreach (BandMemberData data in allBandMembers)
        {
            RecruitableBandMember recruitableBandMember = new()
            {
                id = data.name,
                name = data.memberName,
                talentLevel = data.talentLevel,
                sprite = data.memberSprite,

                recruitThreshold = Random.Range(60, 65)
            };

            allRecruitableBandMembers.Add(recruitableBandMember.id, recruitableBandMember);
        }
    }

    public void AdjustRecruitmentThreshold(string id, float amount)
    {
        GetRecruitableBandMember(id).AdjustRecruitmentThreshold(amount);
    }

    private RecruitableBandMember GetRecruitableBandMember(string id)
    {
        if (allRecruitableBandMembers.TryGetValue(id, out var result))
        {
            return result;
        }
        else
        {
            Debug.LogError("Could not find recruitable band member");
            return null;
        }
    }
}


