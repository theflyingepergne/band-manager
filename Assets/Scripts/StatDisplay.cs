using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatDisplay : MonoBehaviour
{
    //---References---//
    [SerializeField] public StatType thisStat;
    [SerializeField] private TMP_Text statText;

    //---Local References---//
    private BandManager bm;

    //---Events---//
    void OnEnable() => BandManager.OnStatChanged += HandleStatChanged;
    void OnDisable() => BandManager.OnStatChanged -= HandleStatChanged;

    //---Methods---//
    private void Start()
    {
        bm = BandManager.Instance;

        // Initialise UI with values directly from bm
        switch (thisStat)
        {
            case StatType.Money:
                GetStatText(bm.money);
                break;
            case StatType.Chemistry:
                GetStatText(bm.chemistry);
                break;
            case StatType.Fans:
                GetStatText(bm.fans);
                break;
        }
    }

    private void HandleStatChanged(StatType stat, float amount)
    {
        // When stats change, update UI text
        if (thisStat == stat)
        {
            GetStatText(amount);
        }

        // Debug.Log("changed stat in UI");
    }

    private void GetStatText(float amount)
    {
        // Set positive / negative colours
        string color = amount >= 0 ? "white" : "red";

        // Append any stat change text to outcome text
        string sign = amount >= 0 ? "" : "";
        statText.text = $"<color={color}>{sign}{amount}</color>";
    }
}
