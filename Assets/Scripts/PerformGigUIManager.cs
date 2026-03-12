using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PerformGigUIManager : Singleton<PerformGigUIManager>
{
    // UI references
    [SerializeField] private UIDocument performGigUI;
    private VisualElement root;
    private VisualElement setlistUI;

    // Band references
    private List<GameObject> bandMembers = new List<GameObject>();
    private List<SongData> setlist = new List<SongData>();

    public void Start()
    {
        root = performGigUI.rootVisualElement;
        setlistUI = root.Q<ScrollView>("Setlist");

        GetBandMembers();
        PopulateSetlistUI(GetSetlist());

    }

    public void GetBandMembers()
    {
        bandMembers.AddRange(GameObject.FindGameObjectsWithTag("BandMember"));
        // Debug.Log("Found " + bandMembers.Count + " band members in the scene.");
    }

    public List<SongData> GetSetlist()
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
                Debug.Log("Found song by " + data.memberName);
            }
        }
        return setlist;
    }

    public void PopulateSetlistUI(List<SongData> songs)
    {
        // Display the setlist in the UI (for now, just show song names)
        foreach (SongData song in songs)
        {
            Label songLabel = new Label(song.songName);
            setlistUI.Add(songLabel);
        }
    }
}
