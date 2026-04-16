using System.Collections.Generic;
using UnityEngine;

public class GigSetlistUIManager : Singleton<GigSetlistUIManager>
{
    //---References---//
    [Header("UI References")]
    [SerializeField] private RectTransform setlistSongList;
    [SerializeField] private Vector3 pointerAdjustment = new (150f, 0f, 0f);

    [Header("Prefabs")]
    [SerializeField] private GameObject gigSetlistSongWrapper;
    [SerializeField] private GameObject currentSongPointerPrefab;
    private GameObject currentSongPointer;

    //---Local references--//
    private List<SongEntry> setlist = new List<SongEntry>();
    private BandManager bm;

    public void Start()
    {
        // Init local refs
        bm = BandManager.Instance;

        ClearSetlistWrapper();
        PopulateSetlistPanel(GetSetlist());

        GigDirector.Instance.StartGig();
    }

    public List<SongEntry> GetSetlist()
    {
        setlist.Clear();

        if (bm != null)
        {
            setlist = bm.activeSetlist;
        }

        return setlist;
    }

    public void ClearSetlistWrapper()
    {
        for (int i = setlistSongList.childCount - 1; i >= 0; i--)
        {
            Transform child = setlistSongList.GetChild(i);
            child.SetParent(null);
            Destroy(child.gameObject);
        }
    }

    public void PopulateSetlistPanel(List<SongEntry> songs)
    {
        int i = 0;

        // Display the setlist in the UI
        foreach (SongEntry song in songs)
        {
            i++;

            GameObject newSongEntry = Instantiate(gigSetlistSongWrapper, setlistSongList, false);
            GigSetlistSongManager songManager = newSongEntry.GetComponentInChildren<GigSetlistSongManager>();

            if (songManager != null)
            {
                songManager.SetupSong(i, song.songName);
            }
            else
            {
                Debug.Log("Couldnt find song wrapper manager for some reason");
            }
        }
    }

    public void HighlightCurrentSong(int songIndex)
    {
        
        Debug.Log($"Playing {setlist[songIndex].songName}");
        if (currentSongPointer != null)
        {
            Transform currentSong = setlistSongList.GetChild(songIndex).transform;
            MoveSongPointer(currentSong);
        }
        else
        {
            Transform currentSong = setlistSongList.GetChild(songIndex).transform;
            CreateSongPointer(currentSong);
        }
    }

    private void CreateSongPointer(Transform pointerParent)
    {
        currentSongPointer = Instantiate(
            currentSongPointerPrefab,
            pointerParent,
            false);
        
        currentSongPointer.GetComponent<SpriteRenderer>().sortingOrder = 21;
        currentSongPointer.transform.localPosition = Vector3.zero;
        currentSongPointer.transform.localPosition = pointerAdjustment;
        currentSongPointer.transform.localScale = new Vector3(10f, 10f, 10f);
    }

    private void MoveSongPointer(Transform pointerParent)
    {
        currentSongPointer.transform.SetParent(pointerParent);
        currentSongPointer.transform.localPosition = pointerAdjustment;

    }
}
