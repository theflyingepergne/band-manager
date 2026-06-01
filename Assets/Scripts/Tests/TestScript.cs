using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestSongGenerator : MonoBehaviour
{
    //---References---//
    [Header("Data")]
    [SerializeField] private BandMemberData bandMemberData;
    [SerializeField] private GameEventDatabase gameEventDatabase;

    [Header("Config")]
    [SerializeField] private bool autoGenerateTestSongs;

    [Header("GameObjects")]
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

        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            TriggerGameEvent();
        }

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
            TriggerSceneTransition();

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
        if (gameEventPrefab != null)
        {
            GameEventManager gameEventManager = gameEventPrefab.GetComponent<GameEventManager>();
            gameEventManager.SetupEvent(gameEventDatabase.GetRandomEvent());

            bool active = gameEventPrefab.activeSelf;
            gameEventPrefab.SetActive(!active);
        }
        
    }

    void ToggleCalendar()
    {
        calendarCanvas.SetActive(!calendarCanvas.activeSelf);
    }

    void ChangeDate(int amount)
    {
        DateManager.Instance.ChangeDate(amount);
        Debug.Log($"Current Date: {DateManager.Instance.GetDate()}");
    }

    private async void TriggerSceneTransition()
    {
        // 1. Fade to black and wait
        await CameraFade.Instance.DoCameraFade(1f);

        // Do your background logic here (e.g., load next gig, change UI) ...
        // You can use Task.Delay (milliseconds) like this:
        await Task.Delay(500);

        // 2. Fade back in
        await CameraFade.Instance.DoCameraFade(0f);
    }
}



