using UnityEngine;
using UnityEngine.InputSystem;

public class TestSongGenerator : MonoBehaviour
{
    [SerializeField] private BandMemberData bandMemberData;
    [SerializeField] private bool autoGenerateTestSongs;
    [SerializeField] private GameObject gameEventPrefab;

    void Start()
    {
        if (autoGenerateTestSongs == true)
        {
            GenerateTestSongs();
        }
    }

    void Update()
    {
        if (Keyboard.current.tKey.wasPressedThisFrame)
        {
            GenerateTestSongs();
        }

        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            TriggerGameEvent();
        }

    }

    void GenerateTestSongs()
    {
        // Clear existing to avoid duplicates if you press T twice
        BandManager.Instance.songCollection.Clear();

        string[] titles =
        {
            "Bleed English",
            "The Geeta",
            "Sweetness",
            "Hear You Me",
            "Authority Song",
            "My Gus Theory",
            "A Praise Chorus",
            "Lucky Pedro Mint",
            "Work",
            "Always Be Andy"
        };

        for (int i = 0; i < titles.Length; i++)
        {
            float newSongScore = Random.Range(60f, 100f);
            SongEntry newSong = new SongEntry(titles[i], bandMemberData, newSongScore);
            BandManager.Instance.AddSongToCollection(newSong);
        }

        Debug.Log("Generated test songs in BandManager.");
        BandManager.Instance.PrepareSetlist();
    }

    void TriggerGameEvent()
    {
        bool active = gameEventPrefab.activeSelf;
        gameEventPrefab.SetActive(!active);
    }
}

