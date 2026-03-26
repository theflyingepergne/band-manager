using System.Collections.Generic;
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
        // WriteSong(data);
    }

    public void WriteSong(BandMemberData data)
    {
        SongData newSongData = ScriptableObject.CreateInstance<SongData>();
        newSongData.songName = $"{data.memberName}'s Song";

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

        Debug.Log(newSongData.songName);
        Debug.Log("Score: " + newSongData.songScore);
    }
}
