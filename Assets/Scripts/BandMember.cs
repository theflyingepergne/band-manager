using System.Data.Common;
using UnityEngine;

public class BandMember : MonoBehaviour, IClickable
{
    [SerializeField] ScriptableObject bandMemberData;


    public void Start()
    {
        BandMemberData data = bandMemberData as BandMemberData;
        Sprite sprite = data.memberSprite;
        GetComponent<SpriteRenderer>().sprite = sprite;
    }

    public void OnClicked()
    {
        BandMemberData data = bandMemberData as BandMemberData;
        Debug.Log(data.memberName + " was clicked!");
    }
}
