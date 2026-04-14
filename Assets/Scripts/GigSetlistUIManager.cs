using System.Collections.Generic;
using UnityEngine;

public class GigSetlistUIManager : Singleton<GigSetlistUIManager>
{
    //---References---//
    [Header("UI References")]
    [SerializeField] private RectTransform setlistSongList;
    [SerializeField] private GameObject gigSetlistSongWrapper;

    //---Local references--//
    private List<SongEntry> setlist = new List<SongEntry>();
    private BandManager bm;

    public void Start()
    {
        bm = BandManager.Instance;
        PopulateSetlistPanel(GetSetlist());
    }

    public List<SongEntry> GetSetlist()
    {
        setlist.Clear();

        // For now, the entire songCollection is the setlist
        // TODO use the setlist prepared in Transit
        if (bm != null)
        {
            setlist = bm.activeSetlist;
        }

        return setlist;
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
}
