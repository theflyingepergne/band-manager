using UnityEngine;

[System.Serializable]
public class GoMissing : CustomTraitLogic
{
    [Header("Chance to occur")]
    [Range(0f, 1f)]
    [Tooltip
    (
        "Chance from 0 to 1"
        +"\n0 = will never occur"
        +"\n1 = will always occur"
    )]
    public float probability = 1f;

    [Header("Money lost")]
    public StatType stat = StatType.Money;
    public float amount = 5f;

    //---Methods---//
    public override void Execute(BandMemberInstance owner)
    {
        if (owner.bandMember.gameObject.activeSelf)
        {
            // if the band member is active, attempt to "go missing"
            if (Random.value <= probability)
            {
                owner.isAbsent = true;
                owner.bandMember.CheckIsAbsent();

                StatChange statChange = new(stat, amount);
                BandManager.Instance.ApplyStatChange(statChange);
            }
        }
        else
        {
            // if they're already inactive, stop them going missing
            owner.isAbsent = false;
            owner.bandMember.CheckIsAbsent();
        }
    }
}