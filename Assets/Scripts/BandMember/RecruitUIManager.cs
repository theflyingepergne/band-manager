using System.Collections.Generic;
using UnityEngine;

public class RecruitUIManager : MonoBehaviour
{
    //---References---//
    [Header("UI")]
    [SerializeField] private RectTransform recruitBandMemberPanel;

    [Header("Prefabs")]
    [SerializeField] private GameObject newRecruitPrefab;

    //---Local References---//
    private RecruitmentManager rm;

    void Awake()
    {
        rm = RecruitmentManager.Instance;
    }

    private void OnEnable()
    {
        ClearRecruitmentPanel();
        CameraFade.OnFadeInComplete += HandleFadeInComplete;
    }

    private void OnDisable()
    {
        CameraFade.OnFadeInComplete -= HandleFadeInComplete;
    }

    private void HandleFadeInComplete()
    {
        PopulateRecruitBandMemberPanel();
    }

    private void PopulateRecruitBandMemberPanel()
    {
        ClearRecruitmentPanel();

        List<string> currentIds = new();

        for (int i = 0; i < 3; i++)
        {
            RecruitableBandMember recruitable = rm.GetRandomRecruitableBandMember(currentIds);

            GameObject newRecruit = Instantiate(newRecruitPrefab, recruitBandMemberPanel, false);
            NewRecruitManager newRecruitManager = newRecruit.GetComponent<NewRecruitManager>();

            newRecruitManager.SetupNewRecruit(recruitable);
            
            currentIds.Add(recruitable.id);
        }
    }

    private void ClearRecruitmentPanel()
    {
        for (int i = recruitBandMemberPanel.transform.childCount - 1; i >= 0; i--)
        {
            Destroy(recruitBandMemberPanel.transform.GetChild(i).gameObject);
        }
    }
}
