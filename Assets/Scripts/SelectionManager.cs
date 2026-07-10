public class SelectionManager : Singleton<SelectionManager>
{
    private BandMember selectedBandMember;

    public void Select(BandMember bandMember)
    {
        // Already selected
        if (selectedBandMember == bandMember)
        {
            ClearSelection();
            return;
        }

        // Deselect previous
        if (selectedBandMember != null)
            selectedBandMember.OnDeselected();

        // Select new
        selectedBandMember = bandMember;
        selectedBandMember.OnSelected();
    }

    public void ClearSelection()
    {
        if (selectedBandMember != null)
        {
            selectedBandMember.OnDeselected();
            selectedBandMember = null;
        }
    }
}