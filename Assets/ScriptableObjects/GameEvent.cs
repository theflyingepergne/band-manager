using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "GameEvent", menuName = "Scriptable Objects/Game Event")]
public class GameEvent : ScriptableObject
{
    public string eventTitle;
    [TextArea] public string description;
    public Sprite eventIllustration;

    public bool isDecisionEvent; // If false, it's just a notification

    [Header("Choice A")]
    public string choiceALabel;
    public List<StatEffect> choiceAEffects;

    [Header("Choice B (Optional)")]
    public string choiceBLabel;
    public List<StatEffect> choiceBEffects;
}