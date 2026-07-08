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
            BandMemberInstance BandMemberInstance = new(data);

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
    public BandMemberInstance GetBandMemberInstance(string id)
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

    public (string id, BandMemberInstance member)? GetRandomBandMemberInstance(List<string> excludedIds = null)
    {
        IEnumerable<KeyValuePair<string, BandMemberInstance>> candidates = allBandMemberInstances;

        if (excludedIds != null && excludedIds.Count > 0)
        {
            candidates = candidates.Where(pair => !excludedIds.Contains(pair.Key));
        }

        var array = candidates.ToArray();

        if (array.Length == 0) return null;

        var chosen = array[Random.Range(0, array.Length)];

        return (chosen.Key, chosen.Value);
    }
}


