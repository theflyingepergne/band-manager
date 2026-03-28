using System.Collections.Generic;
using UnityEngine;

public class PerformGigUIManager : Singleton<PerformGigUIManager>
{
    // UI references
    [SerializeField] private RectTransform setlistSongList;
    [SerializeField] private GameObject setlistSongWrapper;

    // Band references
    private List<GameObject> bandMembers = new List<GameObject>();
    private List<SongEntry> setlist = new List<SongEntry>();

    public void Start()
    {
        GetBandMembers();
        PopulateSetlistPanel(GetSetlist());
    }

    public void GetBandMembers()
    {
        bandMembers.AddRange(GameObject.FindGameObjectsWithTag("BandMember"));
    }

    public List<SongEntry> GetSetlist()
    {
        setlist.Clear();

        // This will eventually get the setlist chosen by the user for the gig
        // For now, we'll just display all songs written by each band member
        foreach (GameObject member in bandMembers)
        {
            BandMemberData data = member.GetComponent<BandMember>().bandMemberData;
            if (data.songsWritten.Count > 0)
            {
                setlist.AddRange(data.songsWritten);
                // Debug.Log("Found " + data.memberName + "'s song");
            }
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

            GameObject setlistSongEntry = Instantiate(setlistSongWrapper, setlistSongList, false);
            SongWrapperManager songManager = setlistSongEntry.GetComponentInChildren<SongWrapperManager>();

            if (songManager != null)
            {
                // Debug.Log("found song wrapper manager");
                songManager.SetSongNo(i);
                songManager.SetSongName(song.songName);
            }
            else
            {
                Debug.Log("Couldnt find song wrapper manager for some reason");
            }
        }
    }
}
