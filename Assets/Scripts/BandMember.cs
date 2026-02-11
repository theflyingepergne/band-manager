using UnityEngine;

public class BandMember : MonoBehaviour, IClickable
{
    [SerializeField] private BandMemberData bandMemberData;

    public void Start()
    {
        BandMemberData data = bandMemberData;
        Sprite sprite = data.memberSprite;
        GetComponent<SpriteRenderer>().sprite = sprite;
    }

    public void OnClicked()
    {
        BandMemberData data = bandMemberData;
        ViewBandMemberUIManager.Instance.ShowBandMemberDetails(true, data);
    }
}
