using System.Collections.Generic;
using UnityEngine;

public class BandMember : MonoBehaviour, IClickable
{
    [SerializeField] private BandMemberData bandMemberData;

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

        // TEST create a new song
        SongData newSongData = ScriptableObject.CreateInstance<SongData>();
        newSongData.songName = "Kevin's Song";

        // 1. Initialize the lists so they aren't null
        newSongData.songGenres = new List<GenreData>();
        newSongData.songInstruments = new List<InstrumentData>();

        // 2. Pick a random Genre (if the member has any)
        if (data.genres != null && data.genres.Count > 0)
        {
            int randomIndex = Random.Range(0, data.genres.Count);
            newSongData.songGenres.Add(data.genres[randomIndex]);
        }

        // 3. Pick a random Instrument (if the member has any)
        if (data.instruments != null && data.instruments.Count > 0)
        {
            int randomIndex = Random.Range(0, data.instruments.Count);
            newSongData.songInstruments.Add(data.instruments[randomIndex]);
        }

        newSongData.songScore = Random.Range(0f, 100f);

        data.songsWritten.Add(newSongData); // Add the new song to the member's list of songs

        WriteSong(newSongData);
    }

    public void WriteSong(SongData newSongData)
    {
        Debug.Log(newSongData.songName);
        // Debug.Log("Genres: " + string.Join(", ", newSongData.songGenres.ConvertAll(g => g.genreName)));
        // Debug.Log("Instruments: " + string.Join(", ", newSongData.songInstruments.ConvertAll(i => i.instrumentName)));
        Debug.Log("Score: " + newSongData.songScore);
    }
}
