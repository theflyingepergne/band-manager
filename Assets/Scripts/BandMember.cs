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
        ViewBandMemberUIManager.Instance.ShowBandMemberDetails(true, data);
    }
}
