using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class TraitInstance
{
    //---Core Vars---//
    public string traitName;
    public string description;
    public Sprite icon;

    [System.NonSerialized]
    public BandMemberInstance owner;

    public List<CustomTraitCondition> traitConditions;
    public List<CustomTraitLogic> traitLogics;

    // private string systemTime;

    //---Constructor---//
    public TraitInstance(TraitData data = null, BandMemberInstance bandMemberInstance = null)
    {
        traitName = data.traitName;
        description = data.description;
        icon = data.icon;

        owner = bandMemberInstance;

        // Cloner class creates per-owner copies of each trait condition + logic
        traitConditions = data.traitConditions.Select(c => Cloner.CloneFields(c)).ToList();
        traitLogics = data.traitLogics.Select(l => Cloner.CloneFields(l)).ToList();

        // systemTime = System.DateTime.Now.ToString();
    }

    public void InitializeConditions()
    {
        // Debug.Log($"{owner.name}'s trait at {systemTime}");

        foreach (var condition in traitConditions)
        {
            condition.Initialize(owner, this);
            // Debug.Log($"Registered condition: {condition}");
        }
    }

    public void DeinitializeConditions()
    {
        foreach (var condition in traitConditions)
        {
            condition.Unregister();
            // Debug.Log($"Unregistered condition: {condition}");
        }
    }

    public void ExecuteTraitLogic()
    {
        foreach (var logic in traitLogics)
        {
            logic.Execute(owner);
            // Debug.Log($"Executing {logic} belonging to {owner.name}");
        }
    }
}