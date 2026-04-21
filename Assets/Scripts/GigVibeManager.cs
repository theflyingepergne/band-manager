using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class GigVibeManager : Singleton<GigVibeManager>
{
    //---References---//
    [Header("UI References")]
    [SerializeField] private RectTransform vibeBarWrapper;

    [Header("Prefabs")]
    [SerializeField] private GameObject vibeBarPrefab;

    //---Methods---//
    void Start()
    {

    }

    public GameObject SetupVibeBar(SongEntry song)
    {
        GameObject vibeBar = Instantiate(vibeBarPrefab, vibeBarWrapper, false);
        GigVibeBar vibeBarScript = vibeBar.GetComponent<GigVibeBar>();
        vibeBar.transform.localPosition = Vector3.zero;

        vibeBarScript.SetupBar(song.songScore, GigDirector.Instance.songDuration);

        Debug.Log(song.songName);
        return vibeBar;
    }
}
