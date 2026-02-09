using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BandMemberData", menuName = "Scriptable Objects/BandMemberData")]
public class BandMemberData : ScriptableObject
{
    public string memberName;
    public List<string> instruments;
    [Range(0, 10)]
    public int talentLevel;
    public Sprite memberSprite;
    public List<string> genres;
    public List<string> traits;
}
