using UnityEngine;
using TMPro;
using DG.Tweening;

public class StatDisplay : MonoBehaviour
{
    //---References---//
    [SerializeField] public StatType thisStat;
    [SerializeField] private TMP_Text statText;

    //---Events---//
    void OnEnable() => BandManager.OnStatChanged += HandleStatChanged;
    void OnDisable() => BandManager.OnStatChanged -= HandleStatChanged;

    //---Methods---//
    private void Start()
    {
        BandManager bm = BandManager.Instance;

        // Initialise UI with values directly from bm
        switch (thisStat)
        {
            case StatType.Money:
                DisplayStatText(FormatStatText(StatType.Money, bm.money), bm.money);
                break;
            case StatType.Chemistry:
                DisplayStatText(FormatStatText(StatType.Chemistry, bm.chemistry), bm.chemistry);
                break;
            case StatType.Fans:
                DisplayStatText(FormatStatText(StatType.Fans, bm.fans), bm.fans);
                break;
        }

    }

    private void HandleStatChanged(StatType stat, float amount)
    {
        // When stats change, update UI text
        if (thisStat == stat)
        {
            DisplayStatText(FormatStatText(stat, amount), amount);
            statText.transform.DOPunchScale(Vector2.one * 1.2f, 0.2f);
        }
        // Debug.Log("changed stat in UI");
    }

    private string FormatStatText(StatType stat, float amount)
    {
        switch (stat)
        {
            case StatType.Money:
                return amount.ToString();
            case StatType.Chemistry:
                return amount.ToString() + "%";
            case StatType.Fans:
                return FormatUtils.AbbreviateNumber(amount);
            default:
                return amount.ToString();
        }
    }

    private void DisplayStatText(string displayText, float amount)
    {
        // Prepare text for display in the UI

        // Set positive / negative colours
        string color = amount >= 0 ? "white" : "red";

        // Append sign to amount
        string sign = amount >= 0 ? "" : "";
        statText.text = $"<color={color}>{sign}{displayText}</color>";
    }

}
