using UnityEngine;

public class BandSpawner : MonoBehaviour
{
    //---References---//
    [SerializeField] private GameObject bandMemberPrefab;
    [SerializeField] private Vector2 spawnArea = new Vector2(9.5f, 5.1f);
    // [SerializeField] private bool addAmbulate = true;
    Vector3 scale = new Vector3(0.5f, 0.5f, 0.5f);



    //---Methods---//
    void Start()
    {
        SpawnBand();
    }

    void SpawnBand()
    {
        // 1. Check if the BandManager exists and has members
        if (BandManager.Instance == null) return;

        foreach (var data in BandManager.Instance.BandMembers)
        {
            // 2. Pick a random starting spot within the area
            Vector3 randomPos = new Vector3(
                Random.Range(-spawnArea.x, spawnArea.x),
                Random.Range(-spawnArea.y, spawnArea.y),
                0
            );

            // 3. Create the member
            GameObject spawnedBandMember = Instantiate(bandMemberPrefab, randomPos, Quaternion.identity);

            spawnedBandMember.GetComponent<BandMember>().PopulateBandMemberData(data);
            spawnedBandMember.transform.localScale = scale;
            spawnedBandMember.AddComponent<Ambulate>();

            // 4. Inject the boundaries into the Ambulate script
            if (spawnedBandMember.TryGetComponent(out Ambulate ambulate))
            {
                ambulate.SetupBoundaries(spawnArea);
                ambulate.speed = Random.Range(1f, 4f);
            }

            // 5. Inject the Data into your BandMember display script
            // (Assuming you have a script that handles the name/sprite display)
            // newMember.GetComponent<BandMember>().Initialize(data);
        }
    }
}