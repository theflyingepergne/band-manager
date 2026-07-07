using System.Collections.Generic;
using UnityEngine;

public class GigSetlistUIManager : Singleton<GigSetlistUIManager>
{
    //---References---//
    [Header("UI References")]
    [SerializeField] private GameObject setlistPanel;
    [SerializeField] private GameObject vibeBarWrapper;
    [SerializeField] private RectTransform setlistSongList;
    [SerializeField] private Vector3 pointerAdjustment = new (150f, 0f, 0f);

    [Header("Prefabs")]
    [SerializeField] private GameObject gigSetlistSongWrapper;
    [SerializeField] private GameObject currentSongPointerPrefab;
    [SerializeField] private GameObject homeNoGig;
    private GameObject currentSongPointer;

    //---Local references--//
    public List<SongEntry> setlist = new();
    private BandManager bm;

    protected override void Awake()
    {
        base.Awake();

        // Cache local refs
        bm = BandManager.Instance;
    }

    public void SetupGigUI()
    {
        PopulateSetlistPanel(GetSetlist());

        setlistPanel.SetActive(true);
        vibeBarWrapper.SetActive(true);
        homeNoGig.SetActive(false);

        GigDirector.Instance.SetupAllVibeBars();
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

    public void PopulateSetlistPanel(List<SongEntry> songs)
    {
        ClearSetlistWrapper();

        int i = 0;

        // Display the setlist in the UI
        foreach (SongEntry song in songs)
        {
            i++;

            GameObject newSongEntry = Instantiate(gigSetlistSongWrapper, setlistSongList, false);
            GigSetlistSongManager songManager = newSongEntry.GetComponentInChildren<GigSetlistSongManager>();

            if (songManager != null)
            {
                songManager.SetupSong(i, song.name);
            }
            else
            {
                Debug.Log("Couldnt find song wrapper manager for some reason");
            }
        }
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

    public void HighlightCurrentSong(int songIndex)
    {
        
        // Debug.Log($"Playing {setlist[songIndex].songName}");
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
        currentSongPointer.transform.localPosition = pointerAdjustment;
        currentSongPointer.transform.localScale = new Vector3(10f, 10f, 10f);
    }

    private void MoveSongPointer(Transform pointerParent)
    {
        currentSongPointer.transform.SetParent(pointerParent);
        currentSongPointer.transform.localPosition = pointerAdjustment;

    }
}
