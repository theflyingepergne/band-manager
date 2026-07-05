using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class NewRecruitManager : MonoBehaviour
{
    //---References---//
    [Header("UI")]
    [SerializeField] private Image recruitSprite;
    [SerializeField] private TMP_Text recruitName;
    [SerializeField] private TMP_Text recruitInstruments;
    [SerializeField] private TMP_Text recruitTraits;
    [SerializeField] private RectTransform recruitDismissPanel;

    //---Local References---//
    private string id;
    private BandMemberInstance data;

    //---Events---//
    public static System.Action<NewRecruitManager> OnAnyPanelOpened;

    //---Init Methods---//
    private void OnEnable()
    {
        OnAnyPanelOpened += CloseIfNotThis;
        SetRecruitDismissPanelVisible(false);
    }

    private void OnDisable()
    {
        OnAnyPanelOpened -= CloseIfNotThis;
    }

    //---Methods---//
    public void SetupNewRecruit(string newID)
    {
        id = newID;
        data = RecruitmentManager.Instance.GetBandMemberInstance(id);
        recruitSprite.sprite = data.sprite;
        recruitName.text = data.name;
        // recruitInstruments.text = $"Plays {data.instruments[0].instrumentName}";
        // recruitTraits.text = $"Seems {data.traits[0]}...";
    }

    //---Recruit Dismiss Panel---//
    public void ToggleRecruitDismissPanel()
    {
        OnAnyPanelOpened?.Invoke(this);

        SetRecruitDismissPanelVisible(!recruitDismissPanel.gameObject.activeSelf);
    }

    private void SetRecruitDismissPanelVisible(bool visible)
    {
        recruitDismissPanel.gameObject.SetActive(visible);
    }

    public void Recruit()
    {
        BandManager.Instance.RecruitMember(id);
        Destroy(gameObject);
    }

    public void Dismiss()
    {
        Destroy(gameObject);
    }

    private void CloseIfNotThis(NewRecruitManager openedManager)
    {
        if (openedManager != this)
        {
            SetRecruitDismissPanelVisible(false);
        }
    }
}
