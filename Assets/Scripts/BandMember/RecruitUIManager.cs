using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;

public class RecruitUIManager : MonoBehaviour
{
    //---References---//
    [Header("UI")]
    [SerializeField] private RectTransform recruitBandMemberPanel;

    [Header("Prefabs")]
    [SerializeField] private GameObject newRecruitPrefab;

    [Header("Tween controls")]
    [SerializeField] private float strength = 1f;
    [SerializeField] private float duration = 0.4f;
    [SerializeField] private int vibrato = 1;
    [SerializeField] private int elasticity = 1;

    //---Local References---//
    private RecruitmentManager rm;
    private Sequence newRecruitSequence;

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

        // initialize sequence
        newRecruitSequence = DOTween.Sequence();

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


            newRecruitSequence.Append
            (
                newRecruit.transform.DOPunchScale(Vector2.one * strength,
                duration,
                vibrato,
                elasticity)
                .OnStart(newRecruitManager.ShowNewRecruit)
            );

            currentIds.Add(id);
        }

        newRecruitSequence.Play();
    }

    private void ClearRecruitmentPanel()
    {
        newRecruitSequence?.Kill();
        newRecruitSequence = null;

        for (int i = recruitBandMemberPanel.transform.childCount - 1; i >= 0; i--)
        {
            Destroy(recruitBandMemberPanel.transform.GetChild(i).gameObject);
        }
    }
}
