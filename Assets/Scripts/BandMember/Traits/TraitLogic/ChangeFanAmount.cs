[System.Serializable]
public class ChangeFanAmount : CustomTraitLogic
{
    //---References---//
    public int amount;

    //---Methods---//
    public override void Execute(BandMemberInstance owner)
    {
        StatChange statChange = new(StatType.Fans, amount);

        BandManager.Instance.ApplyStatChange(statChange);
    }
}