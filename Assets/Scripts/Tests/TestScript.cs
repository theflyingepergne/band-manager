using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestSongGenerator : MonoBehaviour
{
    //---References---//
    [Header("Data")]
    [SerializeField] private BandMemberInstance bandMemberData;
    [SerializeField] private GameEventDatabase gameEventDatabase;

    [Header("Config")]
    [SerializeField] private bool autoGenerateTestSongs;

    [Header("GameObjects")]
    [SerializeField] private GameEventManager gameEventManager;
    [SerializeField] private GameObject gameEventPrefab;
    [SerializeField] private GameObject calendarCanvas;

    //---Methods---//
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

        // if (Keyboard.current.eKey.wasPressedThisFrame)
        // {
        //     TriggerGameEvent();
        // }

        if (Keyboard.current.cKey.wasPressedThisFrame)
        {
            ToggleCalendar();
        }

        if (Keyboard.current.rightArrowKey.wasPressedThisFrame)
        {
            ChangeDate(1);
        }

        if (Keyboard.current.leftArrowKey.wasPressedThisFrame)
        {
            ChangeDate(-1);
        }

        if (Keyboard.current.fKey.wasPressedThisFrame)
        {
            // this method specifically fades in then out
            // we could change the method so it only fades in and another script could call fade out
            FadeInFadeOut();
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
            "My Gus Theory",
            "Lucky Pedro Mint",
            "Always Be Andy",
            "Hip to be Obedient"
        };

        for (int i = 0; i < titles.Length; i++)
        {
            float newSongScore = Random.Range(60f, 100f);
            SongEntry newSong = new(
                titles[i],
                bandMemberData,
                bandMemberData.genreAffinities,
                newSongScore);

            BandManager.Instance.AddSongToCollection(newSong);
        }

        Debug.Log("Generated test songs in BandManager.");
        BandManager.Instance.PrepareSetlist();
    }

// testing events is broken atm. need to change to scheduled event
    // void TriggerGameEvent()
    // {
    //     if (gameEventManager != null)
    //     {
    //         gameEventManager.SetupEvent(gameEventDatabase.GetRandomEvent());
    //         gameEventPrefab.SetActive(!gameEventPrefab.activeSelf);
    //     }
    // }

    void ToggleCalendar()
    {
        calendarCanvas.SetActive(!calendarCanvas.activeSelf);
    }

    void ChangeDate(int amount)
    {
        DateManager.Instance.ChangeDate(amount);
        // Debug.Log($"Current Date: {DateManager.Instance.date.GetDateAsString()}");
    }

    // Must be an async method
    private async void FadeInFadeOut()
    {
        // Fade to black
        await CameraFade.Instance.DoCameraFade(1f);

        // Run logic here (e.g., load next gig, change UI)
        // Can use Task.Delay (milliseconds) to hold
        await Task.Delay(500);

        // Fade back in
        await CameraFade.Instance.DoCameraFade(0f);
    }
}



