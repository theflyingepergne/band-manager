using UnityEngine;

[System.Serializable]
public class StatChange
{
    public enum StatType { Money, Fans, Chemistry }
    public StatType stat;
    public float amount; // Can be negative or positive

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
        return $"<color={color}>{sign}{amount} {stat}</color>";
    }
}