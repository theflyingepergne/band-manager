using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class NewRecruitManager : MonoBehaviour
{
    //---References---//
    [Header("Data")]
    [SerializeField] private BandMemberData data;

    [Header("UI References")]
    [SerializeField] private Image recruitSprite;
    [SerializeField] private TMP_Text recruitName;
    [SerializeField] private TMP_Text recruiteInstruments;
    [SerializeField] private TMP_Text recruitTraits;
    [SerializeField] private RectTransform recruitDismissPanel;

    //---Events---//
    // Close other recruit panel options if one is opened
    public static Action<NewRecruitManager> OnAnyPanelOpened;
    void OnEnable() => OnAnyPanelOpened += CloseIfNotThis;
    void OnDisable() => OnAnyPanelOpened -= CloseIfNotThis;

    //---Methods---//

    void Start()
    {
        recruitSprite.sprite = data.memberSprite;
        recruitName.text = data.memberName;
        recruiteInstruments.text = $"Plays {data.instruments[0].instrumentName}";
        recruitTraits.text = $"Seems {data.traits[0]}...";

        SetRecruitDismissPanelVisibile(false);
    }

    public void ToggleRecruitDismissPanel()
    {
        OnAnyPanelOpened?.Invoke(this);

        if (recruitDismissPanel.gameObject.activeSelf == true)
        {
            SetRecruitDismissPanelVisibile(false);
            Debug.Log("hiding options");
        }
        else
        {
            SetRecruitDismissPanelVisibile(true);
            Debug.Log("showing options");
        }
    }

    private void SetRecruitDismissPanelVisibile(bool visible)
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

    private void CloseIfNotThis(NewRecruitManager openedManager)
    {
        if (openedManager != this)
        {
            SetRecruitDismissPanelVisibile(false);
        }
    }
}
