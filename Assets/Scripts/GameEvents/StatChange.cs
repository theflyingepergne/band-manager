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
}