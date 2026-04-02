using UnityEngine;

public class BandMember : MonoBehaviour, IClickable
{
    [SerializeField] public BandMemberData bandMemberData;

    public void Start()
    {
        PopulateBandMemberData(bandMemberData);
    }

    public void PopulateBandMemberData(BandMemberData data)
    {
        bandMemberData = data;
        GetComponent<SpriteRenderer>().sprite = data.memberSprite;
    }

    public void OnClicked()
    {
        ViewBandMembersUIManager.Instance.ShowBandMemberDetails(true, bandMemberData);
        
        // TEST: Write a song for this band member when clicked
        WriteSong(bandMemberData);
    }

    public void WriteSong(BandMemberData data)
    {
        string newSongName = $"{data.memberName}'s Song";
        float newSongScore = Random.Range(0f, 100f);

        SongEntry newSongEntry = new SongEntry(newSongName, data, newSongScore);

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

#if UNITY_EDITOR
    UnityEditor.EditorUtility.SetDirty(data);
#endif
// TODO when save load system is implemented, revisit this


        // Debug.Log(newSongEntry.songName);
        // Debug.Log("Song score = " + newSongEntry.songScore);
    }
}