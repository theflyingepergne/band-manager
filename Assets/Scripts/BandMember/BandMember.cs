using UnityEngine;

public class BandMember : MonoBehaviour, IClickable
{
    //---References---//
    public BandMemberInstance bandMemberInstance;
    private bool isBeingViewed = false;

    //---Events---//
    void OnEnable() => ClickScript.OnClickEmptySpace += HandleClickEmptySpace;
    void OnDisable() => ClickScript.OnClickEmptySpace -= HandleClickEmptySpace;


    //---Methods---//
    public void Start()
    {
        PopulateBandMemberInstance(bandMemberInstance);
    }

    public void PopulateBandMemberInstance(BandMemberInstance data)
    {
        bandMemberInstance = data;
        GetComponent<SpriteRenderer>().sprite = data.sprite;
    }

    public void OnClicked()
    {
        if (isBeingViewed == true)
        {
            // If we are already looking at the band member, close the panel
            ViewBandMembersUIManager.Instance.ShowBandMemberDetails(false, bandMemberInstance);

            // If the band member can ambulate, let them resume ambulating
            if (TryGetComponent<Ambulate>(out Ambulate amb))
            {
                amb.doMove = true;
            }

            isBeingViewed = !isBeingViewed;
        }
        else
        {
            // If we are not already looking at band member, open the panel
            ViewBandMembersUIManager.Instance.ShowBandMemberDetails(true, bandMemberInstance);

            // TEST: Write a song for this band member when clicked
            WriteSong(bandMemberInstance);

            // If band member can ambulate, stop them from moving
            if (TryGetComponent<Ambulate>(out Ambulate amb))
            {
                amb.doMove = false;
            }

            isBeingViewed = !isBeingViewed;
        }

    }

    private void HandleClickEmptySpace()
    {
        if (TryGetComponent<Ambulate>(out Ambulate amb))
        {
            amb.doMove = true;
        }

        isBeingViewed = false;
    }

    public void WriteSong(BandMemberInstance data)
    {
        string newSongName = $"{data.name}'s Song";
        float newSongScore = Random.Range(0f, 100f);

        SongEntry newSongEntry = new(newSongName, data, newSongScore);

        // // 1. Initialize the lists so they are valid
        // newSongData.songGenres = new List<GenreData>();
        // newSongData.songInstruments = new List<InstrumentData>();

        // // 2. Pick a random Genre (if the member has any)
        // if (data.genres != null && data.genres.Count > 0)
        // {
        //     int randomIndex = Random.Range(0, data.genres.Count);
        //     newSongData.songGenres.Add(data.genres[randomIndex]);
        // }

        // // 3. Pick a random Instrument (if the member has any)
        // if (data.instruments != null && data.instruments.Count > 0)
        // {
        //     int randomIndex = Random.Range(0, data.instruments.Count);
        //     newSongData.songInstruments.Add(data.instruments[randomIndex]);
        // }

        BandManager.Instance.AddSongToCollection(newSongEntry);

        // Debug.Log(newSongEntry.songName);
        // Debug.Log("Song score = " + newSongEntry.songScore);
    }
}