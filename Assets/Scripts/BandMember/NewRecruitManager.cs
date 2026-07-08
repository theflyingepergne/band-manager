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
        FormatGenreAffinityText();
        // recruitTraits.text = $"Seems {data.traits[0]}...";
    }

    private void FormatGenreAffinityText()
    {
        // Pick a random genre
        var randomGenre = data.genreAffinities.GetRandomGenre();

        // Format text
        if (randomGenre.Value > 5)
        {
            recruitGenres.text = $"Loves {randomGenre.Key}";
        }
        else if (5 >= randomGenre.Value && randomGenre.Value > 0)
        {
            recruitGenres.text = $"Likes {randomGenre.Key}";
        }
        else if (0 >= randomGenre.Value && randomGenre.Value > -5)
        {
            recruitGenres.text = $"Doesn't like {randomGenre.Key}";
        }
        else
        {
            recruitGenres.text = $"Hates {randomGenre.Key}";
        }
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

    //---Button Methods---//
    public void Recruit()
    {
        BandManager.Instance.RecruitMember(id);
        Destroy(gameObject);
    }

    public void Dismiss()
    {
        Destroy(gameObject);
    }
}
