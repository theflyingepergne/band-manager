using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TraitData", menuName = "Traits/TraitData")]
public class TraitData : ScriptableObject
{
    [Header("Core Info")]
    public string traitName;
    [TextArea]
    public string description;
    public Sprite icon;

    [Header("Custom logic")]
    [SerializeReference, SubclassSelector]
    public List<CustomTraitCondition> traitConditions;

    [SerializeReference, SubclassSelector]
    public List<CustomTraitLogic> traitLogics;
}
