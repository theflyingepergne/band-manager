using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BandMemberDatabase", menuName = "Scriptable Objects/BandMemberDatabase")]
public class BandMemberDatabase : ScriptableObject
{
    public List<BandMemberData> recruitableBandMembers;
}
