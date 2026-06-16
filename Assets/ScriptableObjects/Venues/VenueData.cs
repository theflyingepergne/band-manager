using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "VenueData", menuName = "Venues/VenueData")]
public class VenueData : ScriptableObject
{
    [Header("Basic Info")]
    [SerializeField] public new string name;
    [SerializeField] public string description;

    [Header("Travel")]
    [SerializeField] public float distance;

    [Header("Gig Info")]
    [SerializeField] public List<GenreData> genres;
    [SerializeField] public int capacity;
    [SerializeField] public float bookingFee;
    [SerializeField] public float basePay;

    [Header("Visuals")]
    [SerializeField] public Sprite backgroundSprite;
    // Add field for band member stage transforms - not sure how to do this yet
    // maybe a list of transforms that we can assign in the inspector?
}
