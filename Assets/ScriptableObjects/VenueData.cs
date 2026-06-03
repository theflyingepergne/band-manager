using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "VenueData", menuName = "Scriptable Objects/VenueData")]
public class VenueData : ScriptableObject
{
    [SerializeField] public string name;
    [SerializeField] public string description;
    [SerializeField] public List<GenreData> genres;
    [SerializeField] public float distance;
    [SerializeField] public int capacity;
    [SerializeField] public float bookingFee;
    [SerializeField] public float basePay;
    [SerializeField] public Sprite backgroundSprite;
    // Add field for band member stage transforms - not sure how to do this yet
    // maybe a list of transforms that we can assign in the inspector?
}
