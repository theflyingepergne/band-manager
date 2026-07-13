using UnityEngine;

public class BandMember : MonoBehaviour, IClickable
{
    //---References---//
    public BandMemberInstance bandMemberInstance;

    //---Events---//
    void OnEnable() => ClickScript.OnClickEmptySpace += HandleClickEmptySpace;
    void OnDisable() => ClickScript.OnClickEmptySpace -= HandleClickEmptySpace;

    //---Init Methods---//
    public void PopulateBandMemberInstance(BandMemberInstance data)
    {
        bandMemberInstance = data;

        // Debug.Log($"Populating {bandMemberInstance.name}...");
        GetComponent<SpriteRenderer>().sprite = bandMemberInstance.sprite;

        foreach ( var trait in bandMemberInstance.traits)
        {
            trait.InitializeConditions();
        }
    }

    private void OnDestroy()
    {
        foreach (var trait in bandMemberInstance.traits)
        {
            trait.DeinitializeConditions();
        }
    }

    //---Selection---//
    public void OnClicked()
    {
        SelectionManager.Instance.Select(this);
    }

    private void HandleClickEmptySpace()
    {
        SelectionManager.Instance.ClearSelection();
    }

    public void OnSelected()
    {
        ViewBandMembersUIManager.Instance.ShowBandMemberDetails(bandMemberInstance);

        if (TryGetComponent(out Ambulate amb))
            amb.doMove = false;

        WriteSong(bandMemberInstance);

        // Debug.Log($"Viewing {bandMemberInstance.name}");
    }

    public void OnDeselected()
    {
        ViewBandMembersUIManager.Instance.HideBandMemberDetails();

        if (TryGetComponent(out Ambulate amb))
            amb.doMove = true;
    }

    //---Song Writing---//
    public void WriteSong(BandMemberInstance data)
    {
        SongEntry newSongEntry = new(
            $"{data.name}'s Song",
            data,
            data.genreAffinities,
            Random.Range(0f, 100f));

        BandManager.Instance.AddSongToCollection(newSongEntry);
    }
}