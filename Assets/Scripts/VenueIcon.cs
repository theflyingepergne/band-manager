using Unity.Collections;
using UnityEngine;

public class VenueIcon : MonoBehaviour, IClickable
{
    [SerializeField] private VenueData venueData;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public void OnClicked()
    {
        Debug.Log("Venue icon clicked: " + venueData.venueName);
    }
}
