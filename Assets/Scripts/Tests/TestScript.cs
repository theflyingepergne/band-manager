using UnityEngine;
using UnityEngine.InputSystem;

public class TestSongGenerator : MonoBehaviour
{
    [SerializeField] private BandMemberData bandMemberData;
    void Update()
    {
        if (Keyboard.current.tKey.wasPressedThisFrame)
        {
            GenerateTestSongs();
        }
    }

    void GenerateTestSongs()
    {
        // Clear existing to avoid duplicates if you press T twice
        BandManager.Instance.songCollection.Clear();

        string[] titles =
        {
            "Bleed American",
            "The Middle",
            "Sweetness",
            "Hear You Me",
            "Authority Song",
            "My Best Theory",
            "A Praise Chorus",
            "Lucky Denver Mint",
            "Work",
            "Always Be"
        };

        for (int i = 0; i < titles.Length; i++)
        {
            float newSongScore = Random.Range(0f, 100f);
            SongEntry newSong = new SongEntry(titles[i], bandMemberData, newSongScore);
            BandManager.Instance.AddSongToCollection(newSong);
        }

        Debug.Log("Generated test songs in BandManager.");
        BandManager.Instance.PrepareSetlist();
    }
}

