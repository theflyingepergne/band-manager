using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InstrumentManager
{
    //---References---//
    public static InstrumentManager Instance { get; private set; }
    public Dictionary<string, InstrumentInstance> allInstrumentInstances = new();

    //---Local References---//
    private readonly List<InstrumentData> allInstruments;

    //---Methods---//
    public static void Initialize()
    {
        Instance = new();
        Debug.Log("Initialized InstrumentManager");

        Instance.LoadInstrumentDatabase();
        Instance.SetupInstrumentInstances();
    }

    private void LoadInstrumentDatabase()
    {
        InstrumentDatabase database = Resources.Load<InstrumentDatabase>("Databases/InstrumentDatabase");

        if (database != null && database.instruments != null)
        {
            // Copy BandMemberData list from InstrumentDatabase
            Instance.allInstruments.AddRange(database.instruments);
            Debug.Log($"InstrumentManager initialized and loaded {Instance.allInstruments.Count} instruments from the database.");
        }
        else
        {
            Debug.LogError("InstrumentManager Error: Could not find 'InstrumentDatabase' asset in a Resources folder!");
        }
    }

    private void SetupInstrumentInstances()
    {
        foreach (InstrumentData data in allInstruments)
        {
            InstrumentInstance instrumentInstance = new(data);

            allInstrumentInstances.Add(instrumentInstance.id, instrumentInstance);
        }
    }

    public InstrumentInstance GetInstrumentInstance(string id)
    {
        InstrumentInstance instrument;
        if (allInstrumentInstances.TryGetValue(id, out instrument))
        {
            return instrument;
        }
        else
        {
            Debug.LogError($"Could not find instrument with id: {id}");
            return null;
        }
    }
}