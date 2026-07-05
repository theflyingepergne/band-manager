using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/*=================================================================*\
    this class will be responsible for instantiating all possible
    band members that can be recruited

    this class will keep track of progress with recruitable band
    members
\*=================================================================*/

public class RecruitmentManager
{
    //---References---//
    public static RecruitmentManager Instance { get; private set; }
    public Dictionary<string, BandMemberInstance> allBandMemberInstances = new();

    //---Local References---//
    private readonly List<BandMemberData> allBandMembers = new();

    //---Initialization Methods---//
    public static void Initialize()
    {
        Instance = new();
        Debug.Log("Initialized RecruitmentManager");

        Instance.LoadBandMemberInstanceDatabase();

        Instance.SetupBandMemberInstances();
    }

    private void LoadBandMemberInstanceDatabase()
    {
        BandMemberDatabase database = Resources.Load<BandMemberDatabase>("Databases/BandMemberDatabase");

        if (database != null && database.BandMemberInstances != null)
        {
            // Copy BandMemberData list from BandMemberDatabase
            Instance.allBandMembers.AddRange(database.BandMemberInstances);
            Debug.Log($"RecruitmentManager initialized and loaded {Instance.allBandMembers.Count} band members from the database.");
        }
        else
        {
            Debug.LogError("RecruitmentManager Error: Could not find 'BandMemberDatabase' asset in a Resources folder!");
        }
    }

    //---Setup---//
    private void SetupBandMemberInstances()
    {
        foreach (BandMemberData data in allBandMembers)
        {
            // Instantiate BandMemberInstance class using BandMemberData
            BandMemberInstance BandMemberInstance = new()
            {
                id = data.name,
                name = data.memberName,
                talentLevel = data.talentLevel,
                sprite = data.memberSprite,

                recruitThreshold = Random.Range(60, 65)
            };

            // Add BandMemberInstance to list
            allBandMemberInstances.Add(BandMemberInstance.id, BandMemberInstance);
        }
    }

    //---Alterations---//
    public void AdjustRecruitmentThreshold(string id, float amount)
    {
        GetBandMemberInstance(id).AdjustRecruitmentThreshold(amount);
    }

    //--Getters---//
    private BandMemberInstance GetBandMemberInstance(string id)
    {
        if (allBandMemberInstances.TryGetValue(id, out var result))
        {
            return result;
        }
        else
        {
            Debug.LogError("Could not find recruitable band member");
            return null;
        }
    }

    public BandMemberInstance GetRandomBandMemberInstance(List<string> excludedIds = null)
    {
        // get all keys
        IEnumerable<string> validKeys = allBandMemberInstances.Keys;

        if (excludedIds != null || excludedIds.Count > 0)
        {
            // keep only the validKeys that do not contain excludedIds
            validKeys = validKeys.Where(id => !excludedIds.Contains(id));
        }
        
        // create a temporary array which we can use to get random indexes
        string[] keys = validKeys.ToArray();

        if (keys.Length > 0)
        {
            // if the keys array > 0, return a random BandMemberInstance
            return allBandMemberInstances[keys[Random.Range(0, keys.Length)]];
        }
        else
        {
            // otherwise return null
            return null;
        }
    }
}


