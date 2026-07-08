using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InstrumentDatabase", menuName = "Instruments/InstrumentDatabase")]
public class InstrumentDatabase : ScriptableObject
{
    public List<InstrumentData> instruments;
}