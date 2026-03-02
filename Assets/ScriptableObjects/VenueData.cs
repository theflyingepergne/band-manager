using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "VenueData", menuName = "Scriptable Objects/VenueData")]
public class VenueData : ScriptableObject
{
    [SerializeField] public string venueName;
    [SerializeField] private string venueDescription;
    [SerializeField] private List<GenreData> venuePreferredGenres;
    [SerializeField] private int venueCapacity;
    [SerializeField] private Sprite venueBackgroundSprite;
}
