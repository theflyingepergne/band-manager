using UnityEngine;
using UnityEngine.UIElements;


public class BandMember : MonoBehaviour, IClickable
{
    [SerializeField] private BandMemberData bandMemberData;

    public void Start()
    {
        BandMemberData data = bandMemberData as BandMemberData;
        Sprite sprite = data.memberSprite;
        GetComponent<SpriteRenderer>().sprite = sprite;
    }

    public void OnClicked()
    {
        BandMemberData data = bandMemberData;
        ViewBandMembersUIManager.Instance.ShowBandMemberDetails(true, data);
    }

    // binding data to the UI


}
