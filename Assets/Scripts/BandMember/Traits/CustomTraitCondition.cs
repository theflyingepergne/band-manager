[System.Serializable]
public abstract class CustomTraitCondition
{
    [System.NonSerialized]
    protected BandMemberInstance owner;

    [System.NonSerialized]
    protected TraitInstance traitInstanceOuter;

    public virtual void Initialize(BandMemberInstance bandMemberInstance, TraitInstance traitInstance)
    {
        owner = bandMemberInstance;
        traitInstanceOuter = traitInstance;
        Register();
    }

    public abstract void Register();

    public abstract void Unregister();
}