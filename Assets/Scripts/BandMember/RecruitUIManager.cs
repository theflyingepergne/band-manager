using System.Collections.Generic;
using System.Linq;
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

    //---Methods---//
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

        List<string> currentIds = BandManager.Instance.bandMembers.Keys.ToList();

        for (int i = 0; i < 3; i++)
        {
            var result = rm.GetRandomBandMemberInstance(currentIds);

            // if there are no more available band members, stop loop
            if (result == null) break;
            
            var (id, member) = result.Value;

            GameObject newRecruit = Instantiate(newRecruitPrefab, recruitBandMemberPanel, false);
            NewRecruitManager newRecruitManager = newRecruit.GetComponent<NewRecruitManager>();

            newRecruitManager.SetupNewRecruit(id);

            currentIds.Add(id);
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
