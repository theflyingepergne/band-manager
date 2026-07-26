[System.Serializable]
public class ChangeStatAmount : CustomTraitLogic
{
    //---References---//
    public StatType stat;
    public int amount;

    //---Methods---//
    public override void Execute(BandMemberInstance owner)
    {
        StatChange statChange = new(stat, amount);

        BandManager.Instance.ApplyStatChange(statChange);
    }
}