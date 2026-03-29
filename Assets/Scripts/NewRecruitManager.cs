using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class NewRecruitManager : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] private BandMemberData data;

    [Header("UI References")]
    [SerializeField] private Image recruitSprite;
    [SerializeField] private TMP_Text recruitName;
    [SerializeField] private TMP_Text recruiteInstruments;
    [SerializeField] private TMP_Text recruitTraits;
    [SerializeField] private RectTransform recruitDismissPanel;

    public void Start()
    {
        recruitSprite.sprite = data.memberSprite;
        recruitName.text = data.memberName;
        recruiteInstruments.text = $"Plays {data.instruments[0].instrumentName}";
        recruitTraits.text = $"Seems {data.traits[0]}...";
        
        recruitDismissPanel.gameObject.SetActive(false);
    }

    public void ToggleRecruitDismissPanel()
    {
        if (recruitDismissPanel.gameObject.activeSelf == true)
        {
            SetRecruitDismissPanelVisibility(false);
            Debug.Log("hiding options");
        }
        else
        {
            SetRecruitDismissPanelVisibility(true);
            Debug.Log("showing options");
        }
    }

    public void SetRecruitDismissPanelVisibility(bool visible)
    {
        recruitDismissPanel.gameObject.SetActive(visible);
    }

    public void Recruit()
    {
        Debug.Log("Recruited");
    }

    public void Dismiss()
    {
        Debug.Log("Dismissed");
    }
}
