using UnityEngine;

public class BandMember : MonoBehaviour, IClickable
{
    [SerializeField] public BandMemberData bandMemberData;

    public void Start()
    {
        BandMemberData data = bandMemberData;
        Sprite sprite = data.memberSprite;
        GetComponent<SpriteRenderer>().sprite = sprite;
    }

    public void OnClicked()
    {
        BandMemberData data = bandMemberData;
        ViewBandMemberUIManager.Instance.ShowBandMemberDetails(true, data);
        
        // TEST: Write a song for this band member when clicked
        WriteSong(data);
    }

    public void WriteSong(BandMemberData data)
    {
        string newSongName = $"{data.memberName}'s Song";
        float newSongScore = Random.Range(0f, 100f);

        SongEntry newSongEntry = new SongEntry(newSongName, newSongScore);

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

        data.songsWritten.Add(newSongEntry); // Add the new song to the member's list of songs

#if UNITY_EDITOR
    UnityEditor.EditorUtility.SetDirty(data);
#endif
// TODO when save load system is implemented, revisit this


        Debug.Log(newSongEntry.songName);
        Debug.Log("Song score = " + newSongEntry.songScore);
    }
}