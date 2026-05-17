using UnityEngine;

[System.Serializable]
public class StatChange
{
    public StatType stat;
    public float amount; // Can be negative or positive

    public void SetupStatChange(StatType newStat, float newAmount)
    {
        stat = newStat;
        amount = newAmount;
    }

    public void ApplyStatChange()
    {
        BandManager.Instance.ApplyStatChange(this);
    }

    public string GetStatText()
    {
        // Set positive / negative colours
        string color = amount >= 0 ? "green" : "red";

        // Append any stat change text to outcome text
        string sign = amount >= 0 ? "+" : "";

        switch (stat)
        {
            case StatType.Money:
                // Add pound sign
                string poundAmount = amount.ToString("+£#,##0.00;-£#,##0.00");
                string moneyText = $"<color={color}>{poundAmount} {stat}</color>";
                return moneyText;
            case StatType.Fans:
                // Abbreviate number
                string abbrAmount = FormatUtils.AbbreviateNumber(amount);
                string abbrOutText = $"<color={color}>{sign}{abbrAmount} {stat}</color>";
                return abbrOutText;
            case StatType.Chemistry:
                // Add % sign
                string percentAmount = amount.ToString("+0.#;-0.#") + "%";
                string chemistryText = $"<color={color}>{percentAmount} {stat}</color>";
                return chemistryText;
            default:
                return "0";
        }
    }
}