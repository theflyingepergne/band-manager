using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "VenueDatabase", menuName = "Venues/VenueDatabase")]
public class VenueDatabase : ScriptableObject
{
    [SerializeField] public List<VenueData> venues;
}
