using UnityEngine;

[System.Serializable]
public class StatEffect
{
    public enum StatType { Money, Fans, Chemistry }
    public StatType stat;
    public float amount; // Can be negative or positive
}