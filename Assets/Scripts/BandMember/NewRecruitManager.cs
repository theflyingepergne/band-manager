using DG.Tweening;
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
    [SerializeField] private TMP_Text recruitGenres;
    [SerializeField] private TMP_Text recruitTraits;
    [SerializeField] private RectTransform recruitDismissPanel;

    [Header("Tween Config")]
    [SerializeField] private float duration = 0.2f;

    public BandMemberInstance data;

    //---Local References---//
    private string id;
    private NewRecruitDialogue newRecruitDialogue;

    //---Events---//
    public static System.Action<NewRecruitManager> OnAnyPanelOpened;

    //---Init Methods---//
    void Awake()
    {
        newRecruitDialogue = FindAnyObjectByType<NewRecruitDialogue>();
    }
    
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
    public void ShowNewRecruit()
    {
        gameObject.SetActive(true);
    }

    public void SetupNewRecruit(string newID)
    {
        id = newID;
        data = RecruitmentManager.Instance.GetBandMemberInstance(id);
        recruitSprite.sprite = data.sprite;
        recruitName.text = data.name;
        recruitInstruments.text = $"Plays {data.instruments[Random.Range(0, data.instruments.Count - 1)].instrumentName}";
        SetRandomGenreText();
        recruitTraits.text = $"{data.traits[0].traitName}";
    }

    private void SetRandomGenreText()
    {
        var formattedGenres = data.genreAffinities.FormatGenreAffinities();
        int i = formattedGenres.Count;

        recruitGenres.text = formattedGenres[Random.Range(0, i)];
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

    private void CloseIfNotThis(NewRecruitManager openedManager)
    {
        if (openedManager != this)
        {
            SetRecruitDismissPanelVisible(false);
        }
    }

    public void Recruit()
    {
        BandManager.Instance.RecruitMember(id);

        // pop out animation
        Sequence popOut = DOTween.Sequence();
        popOut.Append(transform
            .DOScale(Vector2.zero, duration)
            .SetEase(Ease.InBack));
        popOut.AppendInterval(0.2f);
        popOut.OnComplete(() => Destroy(gameObject));
    }

    //---Button Methods---//
    public void StartDialogue()
    {
        newRecruitDialogue.PrepareDialogue(data.inkRecruitmentDialogue, data.sprite, this);
    }

    public void Dismiss()
    {
        Destroy(gameObject);
    }
}
