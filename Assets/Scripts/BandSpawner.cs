using UnityEngine;

public class BandSpawner : MonoBehaviour
{
    //---References---//
    [SerializeField] private GameObject bandMemberPrefab;
    [SerializeField] private Vector2 spawnArea = new Vector2(9.5f, 5.1f);
    [Tooltip("If ticked, spawned band members will ambulate")]
    [SerializeField] private bool shouldAmbulate = true;
    Vector3 scale = new Vector3(0.5f, 0.5f, 0.5f);



    //---Methods---//
    void Start()
    {
        SpawnBand();
    }

    void SpawnBand()
    {
        // Check if the BandManager exists and has members
        if (BandManager.Instance == null) return;

        foreach (var data in BandManager.Instance.BandMembers)
        {
            // Pick a random starting spot within the area
            Vector3 randomPos = new Vector3(Random.Range(-spawnArea.x, spawnArea.x), Random.Range(-spawnArea.y, spawnArea.y), 0);

            // Instantiate band member prefab
            GameObject spawnedBandMember = Instantiate(bandMemberPrefab, randomPos, Quaternion.identity);

            spawnedBandMember.GetComponent<BandMember>().PopulateBandMemberData(data);
            spawnedBandMember.transform.localScale = scale;

            // Set up ambulation
            if (shouldAmbulate)
            {
                Ambulate sbmAmbulate = spawnedBandMember.AddComponent<Ambulate>();
                sbmAmbulate.SetupBoundaries(spawnArea);
                sbmAmbulate.speed = Random.Range(1f, 4f);

            }
        }
    }
}