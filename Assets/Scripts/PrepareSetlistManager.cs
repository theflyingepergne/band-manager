using UnityEngine;
using DG.Tweening;
using System;
using System.Collections.Generic;

public class PrepareSetlistManager : Singleton<PrepareSetlistManager>, IHoverable
{
    //---Serialize References---//
    [Header("UI")]
    [SerializeField] private RectTransform setlistWrapper;

    [Header("Prefabs")]
    [SerializeField] private GameObject prepSetlistSongWrapper;
    private GameObject newPrepSetlistSong;
    [SerializeField] private GameObject prepSetlistGhostSong;
    private GameObject newPrepSetlistGhostSong;

    [Header("Positions")]
    [SerializeField] private Transform startAnchor;
    [SerializeField] private Transform hoverAnchor;

    [Header("Animation")]
    [SerializeField] private float tweenSpeed = 0.5f;
    [SerializeField] private Ease easeType = Ease.OutBack;

    //---Local References---//
    private bool isTweening = false;
    private bool currentHoverState = false;
    private List<SongEntry> activeSetlistInitial = new List<SongEntry>();
    private BandManager bm;

    //---Events---//
    public static event Action OnSetlistReordered;

    //---Methods---//
    public void Start()
    {
        bm = BandManager.Instance;

        transform.position = startAnchor.position;
        transform.rotation = startAnchor.rotation;

        SetupSetlist();
    }

    public void SetupSetlist()
    {
        ClearSetlistWrapper();

        // Tell BandManager to pick 6 random songs for activeSetlist
        bm.PrepareSetlist();

        // Get activeSetlist from BandManager
        if (bm.activeSetlist.Count > 0)
        {
            activeSetlistInitial = bm.activeSetlist;

            foreach (SongEntry song in activeSetlistInitial)
            {
                // Instantiate PrepSetlistSongWrapper prefabs
                // Add prefabs to wrapper
                newPrepSetlistSong = Instantiate(prepSetlistSongWrapper, setlistWrapper, false);
                newPrepSetlistSong.GetComponent<PrepareSetlistSongManager>().SetupSong(song);
            }
        }

        BroadcastReorder();
    }

    private void ClearSetlistWrapper()
    {
        for (int i = setlistWrapper.childCount - 1; i >= 0; i--)
        {
            Transform child = setlistWrapper.GetChild(i);
            child.SetParent(null);
            Destroy(child.gameObject);
        }
    }

    //---Tween Hovering---//
    public void OnIsHovering(bool isHovering)
    {
        // If we are already tween, don't interrupt tween
        if (isTweening) return;

        //  If the state hasn't actually changed, do nothing
        if (isHovering == currentHoverState) return;

        currentHoverState = isHovering;
        ExecuteTween(isHovering);
    }

    private void ExecuteTween(bool isHovering)
    {
        // Use the position and rotation of the anchor objects as target pos & rot
        Vector3 targetPos = isHovering ? hoverAnchor.position : startAnchor.position;
        Vector3 targetRot = isHovering ? hoverAnchor.eulerAngles : startAnchor.eulerAngles;

        isTweening = true;
        Sequence seq = DOTween.Sequence();

        seq.Join(transform.DOMove(targetPos, tweenSpeed).SetEase(easeType));
        seq.Join(transform.DORotate(targetRot, tweenSpeed).SetEase(easeType));

        seq.OnComplete(() => isTweening = false);
    }

    //---Setlist Reordering---//
    private int currentHoveredIndex = -1;
    public int GetCurrentHoverIndex() => currentHoveredIndex;

    public void SetCurrentHoverIndex(int index)
    {
        currentHoveredIndex = index;
    }

    public void BroadcastReorder()
    {
        OnSetlistReordered?.Invoke();
        // Debug.Log("Telling everyone to update their indices");
    }

    public void FinalizeSetlist()
    {
        // call this to "save" the setlist to the bandmanager
        List<SongEntry> reorderedSetlist = new List<SongEntry>();

        foreach (Transform child in setlistWrapper)
        {
            var songScript = child.GetComponent<PrepareSetlistSongManager>();
            reorderedSetlist.Add(songScript.song);
        }

        bm.activeSetlist = reorderedSetlist;
    }

    //---Setlist Ghost Slot---//
    public void EnableGhostSong(int index)
    {
        newPrepSetlistGhostSong = Instantiate(prepSetlistGhostSong, setlistWrapper, false);
        // Debug.Log("instantiated ghost song");

        newPrepSetlistGhostSong.transform.SetSiblingIndex(index);
    }

    public void MoveGhostSong(int newIndex)
    {
        if (newPrepSetlistGhostSong != null)
        {
            newPrepSetlistGhostSong.transform.SetSiblingIndex(newIndex);
        }
    }

    public void DisableGhostSong()
    {
        newPrepSetlistGhostSong.transform.SetParent(null);
        Destroy(newPrepSetlistGhostSong);
    }
}