using System;
using System.Collections.Generic;
using Unity.Properties;
using UnityEngine;

[CreateAssetMenu(fileName = "BandMemberData", menuName = "Scriptable Objects/BandMemberData")]
public class BandMemberData : ScriptableObject
{
    public string memberName;
    public List<string> instruments;

    [Range(0, 10)]
    [SerializeField] private int _talentLevel;

    // 2. Point the Property to that variable
    [CreateProperty]
    public int talentLevel 
    { 
        get => _talentLevel; 
        set => _talentLevel = value; 
    }
    public Sprite memberSprite;
    public List<string> genres;
    public List<string> traits;
}
